namespace IEPFiles
{
    public class OrderInfo
    {
        public Guid OrderID { get; set;}
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string ItemName { get; set;}
        public int QuantityOrdered { get; set; }

        // Queue message specifics, works with Azure Storage Queue
        public string QueueMessageId { get; set; }
        public string QueuePopReciept { get; set; }
    }
}