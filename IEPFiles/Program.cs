using IEPFiles;
using Coravel;
using Serilog;

// creating the Serilog ILogger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices(services =>
    {
        //services.AddHostedService<Worker>();
        services.AddScheduler();
        services.AddTransient<ProcessOrder>();
    })
    .Build();

host.Services.UseScheduler(schedule =>
{
    var jobSchedule = schedule.Schedule<ProcessOrder>();
    // when and how our job will run
    // jobSchedule.EveryFiveMinutes().Weekday(); // example
    
    jobSchedule.EverySeconds(2).PreventOverlapping("ProcessOrderJob");
});

await host.RunAsync();
