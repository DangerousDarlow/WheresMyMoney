namespace WheresMyMoney;

public interface ICommandProcessor
{
    Type ProcessesCommand { get; }
}

public interface ICommandProcessor<in T>
{
    void ProcessCommand(T command);
}

public abstract class CommandProcessor<T> : ICommandProcessor, ICommandProcessor<T>
{
    public Type ProcessesCommand { get; } = typeof(T);
    public abstract void ProcessCommand(T command);
}