namespace WheresMyMoney.Import;

public record ImportCommand(string Account, string[] Files);