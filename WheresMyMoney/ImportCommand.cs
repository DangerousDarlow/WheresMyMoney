namespace WheresMyMoney;

public record ImportCommand(string Account, string[] Files);