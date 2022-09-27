using System.CommandLine;

namespace WheresMyMoney;

public class ConsoleApplication
{
    private readonly ICommandProcessor _commandProcessor;

    public ConsoleApplication(ICommandProcessor commandProcessor)
    {
        _commandProcessor = commandProcessor;
    }

    public Task<int> Run(string[] args)
    {
        return new RootCommand("Financial transaction analysis")
            .AddImportCommand(_commandProcessor)
            .InvokeAsync(args);
    }
}

internal static class RootCommandExtensions
{
    public static RootCommand AddImportCommand(this RootCommand rootCommand, ICommandProcessor commandProcessor)
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
        importCommand.SetHandler((account, files) => { commandProcessor.ProcessCommand(new ImportCommand(Account: account, Files: files)); }, accountOption, filesArgument);

        rootCommand.AddCommand(importCommand);
        return rootCommand;
    }
}