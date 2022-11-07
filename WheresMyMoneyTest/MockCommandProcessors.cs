using WheresMyMoney;
using WheresMyMoney.Import;

namespace WheresMyMoneyTest;

public abstract class MockCommandProcessor<T> : CommandProcessor<T>
{
    public IList<T> Commands { get; } = new List<T>();

    public override void ProcessCommand(T command)
    {
        Commands.Add(command);
    }
}

public class MockImportCommandProcessor : MockCommandProcessor<ImportCommand>
{
}