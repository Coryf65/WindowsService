﻿namespace IEPFiles
{
    public interface IOrderConnector
    {
        Task<OrderInfo> GetNextOrder();
        Task RemoveOrder(OrderInfo order);
    }
}
