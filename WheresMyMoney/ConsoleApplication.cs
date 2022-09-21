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
            .AddLoadCommand(_commandProcessor)
            .InvokeAsync(args);
    }
}

internal static class RootCommandExtensions
{
    public static RootCommand AddLoadCommand(this RootCommand rootCommand, ICommandProcessor commandProcessor)
    {
        var accountOption = new Option<string>("account", "Account associated with transactions")
        {
            IsRequired = true
        };

        var filesArgument = new Argument<string[]>("files", "Transaction files")
        {
            Arity = ArgumentArity.OneOrMore
        };

        var loadCommand = new Command("load", "Load transactions from CSV file");
        loadCommand.AddOption(accountOption);
        loadCommand.AddArgument(filesArgument);
        loadCommand.SetHandler((account, files) => { commandProcessor.ProcessCommand(new LoadCommand(Account: account, Files: files)); }, accountOption, filesArgument);

        rootCommand.AddCommand(loadCommand);
        return rootCommand;
    }
}