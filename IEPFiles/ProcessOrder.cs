using Coravel.Invocable;

namespace IEPFiles
{
    internal class ProcessOrder : IInvocable
    {
        private readonly ILogger _logger;

        public ProcessOrder(ILogger<ProcessOrder> logger)
        {
            _logger = logger;
        }

        // Called by the coravel task scheduler when needed
        public async Task Invoke()
        {
            // creating a dummy order
            var order = new OrderInfo
            {
                OrderID = Guid.NewGuid(),
                ItemName = "BBQ Chicken Pizza",
                QuantityOrdered = 1
            };

            // using serilog to serialize our object
            _logger.LogInformation("Processing order {@order}", order);

            //var jobId = Guid.NewGuid();
            //_logger.LogInformation($"Starting job id {jobId}");

            //await Task.Delay(3000); // wait 3 seconds
            //_logger.LogWarning("some Error happened");
            //_logger.LogInformation($"job with id: {jobId} is completed.");

            // return Task.FromResult(true);
        }
    }
}
