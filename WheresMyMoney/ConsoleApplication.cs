using System.CommandLine;
using WheresMyMoney.Import;

namespace WheresMyMoney;

public class ConsoleApplication
{
    private readonly Dictionary<Type, ICommandProcessor> _commandProcessors;

    public ConsoleApplication(IEnumerable<ICommandProcessor> commandProcessors)
    {
        _commandProcessors = commandProcessors.ToDictionary(processor => processor.ProcessesCommand);
    }

    public Task<int> Run(string[] args)
    {
        return new RootCommand("Financial transaction analysis")
            .AddImportCommand(_commandProcessors[typeof(ImportCommand)] as ICommandProcessor<ImportCommand> ??
                              throw new InvalidOperationException("Unable to cast command processor to templated type"))
            .InvokeAsync(args);
    }
}

internal static class RootCommandExtensions
{
    public static RootCommand AddImportCommand(this RootCommand rootCommand, ICommandProcessor<ImportCommand> commandProcessor)
    {
        var accountOption = new Option<string>("account", "Account associated with transactions")
        {
            IsRequired = true
        };

        var filesArgument = new Argument<string[]>("files", "Transaction files")
        {
            Arity = ArgumentArity.OneOrMore
        };

        var importCommand = new Command("import", "Import transactions from CSV file");
        importCommand.AddOption(accountOption);
        importCommand.AddArgument(filesArgument);
        importCommand.SetHandler((account, files) => { commandProcessor.ProcessCommand(new ImportCommand(account, files)); }, accountOption, filesArgument);

        rootCommand.AddCommand(importCommand);
        return rootCommand;
    }
}