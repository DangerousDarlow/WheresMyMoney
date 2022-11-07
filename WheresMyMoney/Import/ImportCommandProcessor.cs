using System.Globalization;
using CsvHelper;

namespace WheresMyMoney.Import;

public class ImportCommandProcessor : CommandProcessor<ImportCommand>
{
    private readonly IStreamReaderFactory _streamReaderFactory;

    public ImportCommandProcessor(IStreamReaderFactory streamReaderFactory)
    {
        _streamReaderFactory = streamReaderFactory;
    }

    public override void ProcessCommand(ImportCommand command)
    {
        foreach (var path in command.Files)
        {
            using var streamReader = _streamReaderFactory.Open(path);
            using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            
            // I would have liked to use GetRecordsAsync but I couldn't get it to work
            var transactions = csvReader.GetRecords<Transaction>();
            foreach (var (timestamp, description, amount, tags) in transactions)
            {
                
            }
        }
    }
}