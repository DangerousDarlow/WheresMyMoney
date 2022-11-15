using Microsoft.Extensions.Logging;
using Npgsql;

namespace WheresMyMoney;

public class PostgresTransactionsRepository : ITransactionsRepository
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly ILogger<PostgresTransactionsRepository> _logger;

    public PostgresTransactionsRepository(NpgsqlDataSource dataSource, ILogger<PostgresTransactionsRepository> logger)
    {
        _dataSource = dataSource;
        _logger = logger;
    }

    public async Task Insert(IReadOnlyCollection<Import.Transaction> transactions)
    {
        await using var connection = await _dataSource.OpenConnectionAsync();
        await using var batch = new NpgsqlBatch(connection);
        var added = DateTime.UtcNow;

        foreach (var transaction in transactions)
        {
            var command = new NpgsqlBatchCommand(
                """
                INSERT INTO transactions(uuid, timestamp, amount, description, added, account)
                VALUES (@uuid, @timestamp, @amount, @description, @added, @account)
                ON CONFLICT DO NOTHING
                """
            );

            command.Parameters.AddWithValue("uuid", Guid.NewGuid());
            command.Parameters.AddWithValue("timestamp", transaction.Timestamp);
            command.Parameters.AddWithValue("amount", (long) (transaction.Amount * 1000000));
            command.Parameters.AddWithValue("description", transaction.Description);
            command.Parameters.AddWithValue("added", added);
            command.Parameters.AddWithValue("account", "Test");

            batch.BatchCommands.Add(command);
        }

        var modified = await batch.ExecuteNonQueryAsync();
        _logger.LogInformation("Inserted {Modified} transactions out of {Total}", modified, transactions.Count);
    }
}