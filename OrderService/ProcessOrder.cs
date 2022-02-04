using Coravel.Invocable;

namespace IEPFiles
{
    internal class ProcessOrder : IInvocable
    {
        private readonly ILogger _logger;
        private readonly IOrderConnector _orderConnector;

        public ProcessOrder(ILogger<ProcessOrder> logger, IOrderConnector orderConnector)
        {
            _logger = logger;
            _orderConnector = orderConnector;
        }

        // Called by the coravel task scheduler when needed
        public async Task Invoke()
        {
            var nextOrder = await _orderConnector.GetNextOrder();

            if (nextOrder is not null)
            {
                _logger.LogInformation("Processing order {@order}", nextOrder);

                // TODO: add order processing

                // tell azure we finished the order
                await _orderConnector.RemoveOrder(nextOrder);
            }
        }
    }
}
