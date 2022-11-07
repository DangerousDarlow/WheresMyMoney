using Microsoft.Extensions.DependencyInjection;
using WheresMyMoney.Import;

namespace WheresMyMoney;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ConsoleApplication>();
        serviceCollection.AddSingleton<ICommandProcessor, ImportCommandProcessor>();
        serviceCollection.AddSingleton<IStreamReaderFactory, StreamReaderFactory>();
        return serviceCollection;
    }
}