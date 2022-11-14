using System.Globalization;
using CsvHelper;

namespace WheresMyMoney.Import;

public class ImportCommandProcessor : CommandProcessor<ImportCommand>
{
    private readonly IStreamReaderFactory _streamReaderFactory;
    private readonly ITransactionsRepository _transactionsRepository;

    public ImportCommandProcessor(IStreamReaderFactory streamReaderFactory, ITransactionsRepository transactionsRepository)
    {
        _streamReaderFactory = streamReaderFactory;
        _transactionsRepository = transactionsRepository;
    }

    public override async Task ProcessCommand(ImportCommand command)
    {
        foreach (var path in command.Files)
        {
            var transactions = await ReadTransactionsFromFile(path);
            await _transactionsRepository.Insert(transactions);
        }
    }

    private async Task<List<Transaction>> ReadTransactionsFromFile(string path)
    {
        using var streamReader = _streamReaderFactory.Open(path);
        using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
        return await csvReader.GetRecordsAsync<Transaction>().ToListAsync();
    }
}