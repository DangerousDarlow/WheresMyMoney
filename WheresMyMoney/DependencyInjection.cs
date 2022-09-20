using Microsoft.Extensions.DependencyInjection;

namespace WheresMyMoney;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ConsoleApplication>();
        serviceCollection.AddSingleton<ICommandProcessor, CommandProcessor>();
        return serviceCollection;
    }
}