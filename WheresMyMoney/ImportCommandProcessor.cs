namespace WheresMyMoney;

public class ImportCommandProcessor : CommandProcessor<ImportCommand>
{
    public override Task ProcessCommand(ImportCommand command)
    {
        return Task.CompletedTask;
    }
}