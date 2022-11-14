using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using WheresMyMoney.Import;

namespace WheresMyMoney;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ConsoleApplication>();
        serviceCollection.AddSingleton<ICommandProcessor, ImportCommandProcessor>();
        serviceCollection.AddSingleton<IStreamReaderFactory, StreamReaderFactory>();
        serviceCollection.AddSingleton<ITransactionsRepository, PostgresTransactionsRepository>();

        var configuration = serviceCollection.BuildServiceProvider().GetService<IConfiguration>();
        var postgresHost = configuration.GetValue<string>("PGHOST");
        var postgresUser = configuration.GetValue<string>("PGUSER");
        var postgresPassword = configuration.GetValue<string>("PGPASSWORD");

        serviceCollection.AddSingleton(NpgsqlDataSource.Create(new NpgsqlConnectionStringBuilder
        {
            Host = postgresHost,
            Username = postgresUser,
            Password = postgresPassword,
            Database = "money"
        }.ConnectionString));

        return serviceCollection;
    }
}