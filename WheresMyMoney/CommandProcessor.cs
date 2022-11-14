namespace WheresMyMoney;

public interface ICommandProcessor
{
    Type ProcessesCommand { get; }
}

public interface ICommandProcessor<in T>
{
    Task ProcessCommand(T command);
}

public abstract class CommandProcessor<T> : ICommandProcessor, ICommandProcessor<T>
{
    public Type ProcessesCommand { get; } = typeof(T);
    public abstract Task ProcessCommand(T command);
}