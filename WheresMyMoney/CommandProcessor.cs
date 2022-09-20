namespace WheresMyMoney;

public interface ICommandProcessor
{
    public Task ProcessCommand(LoadCommand command);
}

public class CommandProcessor : ICommandProcessor
{
    public Task ProcessCommand(LoadCommand command)
    {
        Console.WriteLine("Load");
        return Task.CompletedTask;
    }
}