using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WheresMyMoney;

await Host
    .CreateDefaultBuilder()
    .ConfigureAppConfiguration(builder => builder
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables())
    .ConfigureServices(collection => collection
        .AddLogging()
        .ConfigureDependencyInjection()
    )
    .Build()
    .Services
    .GetService<ConsoleApplication>()
    ?.Run(args)!;