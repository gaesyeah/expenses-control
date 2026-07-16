namespace ExpensesControl.Models;

public record TransactionResponse(Guid Id, string Description, decimal Value, TransactionType Type, Guid PersonId)
{
  public TransactionResponse(TransactionModel transaction)
    : this(transaction.Id, transaction.Description, transaction.Value, transaction.Type, transaction.PersonId) { }
};