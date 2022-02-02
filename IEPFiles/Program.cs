using IEPFiles;
using Coravel;

IHost host = Host.CreateDefaultBuilder(args)
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
    
    jobSchedule.EverySeconds(2);
});

await host.RunAsync();
