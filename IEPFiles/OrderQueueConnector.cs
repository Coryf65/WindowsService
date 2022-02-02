namespace IEPFiles
{
    public class OrderQueueConnector : IOrderConnector
    {
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
