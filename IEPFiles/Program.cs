using IEPFiles;
using Coravel;
using Serilog;

// creating the Serilog ILogger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(path: "logs/logs.txt",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                rollingInterval: RollingInterval.Day) // setting file log levels to the file
    .WriteTo.File(path: "logs/errors.txt",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning) // will only write Errors to this file
    .CreateLogger();


try
{
    Log.Information("Starting service...");

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
}
catch (Exception error)
{
    // log any errors while setting up the host
    Log.Fatal(error, "Exception while setting up Host and scheduler");
}finally
{
    // cleanup
    Log.Information("Exiting service...");
    Log.CloseAndFlush();
}



