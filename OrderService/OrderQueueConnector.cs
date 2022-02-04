using Azure.Storage.Queues;

namespace IEPFiles
{
    public class OrderQueueConnector : IOrderConnector
    {
        private readonly ILogger<OrderQueueConnector> _logger;
        private readonly QueueClient _orderQueueClient;
        private readonly QueueClient _errorQueue;

        public OrderQueueConnector(ILogger<OrderQueueConnector> logger)
        {
            _logger = logger;
            var connectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION"); // using .env file
            var queueOptions = new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 };

            // our orders queue we want to process
            _orderQueueClient = new QueueClient(connectionString, "customer-orders", queueOptions);
            _orderQueueClient.CreateIfNotExists();

            // the bad or errors that occur in processing our other queue
            _errorQueue = new QueueClient(connectionString, "error-customer-orders", queueOptions);
            _errorQueue.CreateIfNotExists();
        }

        public async Task<OrderInfo> GetNextOrder()
        {
            //check for a message, in JSON, de-serialize
            var response = await _orderQueueClient.ReceiveMessageAsync(); // returns 200 ok

            if (response.Value is not null)
            {
                try
                {
                    var order = response.Value.Body.ToObjectFromJson<OrderInfo>();
                    order.QueueMessageId = response.Value.MessageId;
                    order.QueuePopReciept = response.Value.PopReceipt;

                    return order;
                }
                catch (Exception error)
                {
                    _logger.LogError(error, "Error in deserializing the message", response.Value);

                    // put into error queue
                    await _errorQueue.SendMessageAsync(response.Value.Body);
                    // remove from order queue
                    await _orderQueueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
                }                
            }
            return null;
        }

        public async Task RemoveOrder(OrderInfo order)
        {
            // remove form the queue
            await _orderQueueClient.DeleteMessageAsync(order.QueueMessageId, order.QueuePopReciept);
        }
    }
}
