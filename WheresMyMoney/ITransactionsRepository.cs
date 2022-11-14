namespace WheresMyMoney;

public interface ITransactionsRepository
{
    Task Insert(IReadOnlyCollection<Import.Transaction> transactions);
}