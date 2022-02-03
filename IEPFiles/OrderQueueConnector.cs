namespace IEPFiles
{
    public class OrderQueueConnector : IOrderConnector
    {
        private readonly ILogger<OrderQueueConnector> logger;

        public Task<OrderInfo> GetNextOrder()
        {
            throw new NotImplementedException();
        }

        public Task RemoveOrder(OrderInfo order)
        {
            throw new NotImplementedException();
        }
    }
}
