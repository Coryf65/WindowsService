using Azure.Storage.Queues;

namespace IEPFiles
{
    public class OrderQueueConnector : IOrderConnector
    {
        private readonly ILogger<OrderQueueConnector> _logger;
        private readonly QueueClient _orderQueueClient;

        public OrderQueueConnector(ILogger<OrderQueueConnector> logger)
        {
            _logger = logger;
            var connectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION"); // using .env file
            var queueOptions = new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 };

            _orderQueueClient = new QueueClient(connectionString, "customer-orders", queueOptions);
            _orderQueueClient.CreateIfNotExists();
        }

        public async Task<OrderInfo> GetNextOrder()
        {
            //check for a message, in JSON, de-serialize
            var response = await _orderQueueClient.ReceiveMessageAsync(); // returns 200 ok

            if (response.Value is not null)
            {
                var order = response.Value.Body.ToObjectFromJson<OrderInfo>();
                order.QueueMessageId = response.Value.MessageId;
                order.QueuePopReciept = response.Value.PopReceipt;

                return order;
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
