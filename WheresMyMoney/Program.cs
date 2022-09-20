using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WheresMyMoney;

await Host
    .CreateDefaultBuilder()
    .ConfigureServices(collection => collection
        .AddLogging()
        .ConfigureDependencyInjection()
    )
    .Build()
    .Services
    .GetService<ConsoleApplication>()
    ?.Run(args)!;