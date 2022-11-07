namespace WheresMyMoney.Import;

public record Transaction(DateTimeOffset Timestamp, string Description, decimal Amount, string Tags);