using WheresMyMoney;

namespace WheresMyMoneyTest;

public abstract class MockCommandProcessor<T> : CommandProcessor<T>
{
    public IList<T> Commands { get; } = new List<T>();

    public override Task ProcessCommand(T command)
    {
        Commands.Add(command);
        return Task.CompletedTask;
    }
}

public class MockImportCommandProcessor : MockCommandProcessor<ImportCommand>
{
}