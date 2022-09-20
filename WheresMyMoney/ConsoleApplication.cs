using System.CommandLine;

namespace WheresMyMoney;

public class ConsoleApplication
{
    public Task Run(string[] args)
    {
        return new RootCommand("Financial transaction analysis")
            .AddLoadCommand()
            .InvokeAsync(args);
    }
}

internal static class RootCommandExtensions
{
    public static RootCommand AddLoadCommand(this RootCommand rootCommand)
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
        loadCommand.SetHandler((account, files) =>
        {
            Console.WriteLine($"account {account}, files {files[0]}");
        }, accountOption, filesArgument);

        rootCommand.AddCommand(loadCommand);
        return rootCommand;
    }
}