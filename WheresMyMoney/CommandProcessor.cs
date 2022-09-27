namespace WheresMyMoney;

public interface ICommandProcessor
{
    public Task ProcessCommand(ImportCommand command);
}

public class CommandProcessor : ICommandProcessor
{
    public Task ProcessCommand(ImportCommand command)
    {
        Console.WriteLine("Import");
        return Task.CompletedTask;
    }
}