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
            var jobId = Guid.NewGuid();
            _logger.LogInformation($"Starting job id {jobId}");

            await Task.Delay(3000); // wait 3 seconds

            _logger.LogInformation($"job with id: {jobId} is completed.");

            // return Task.FromResult(true);
        }
    }
}
