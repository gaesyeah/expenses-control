namespace ExpensesControl.Models;

public record TransactionResponse(Guid Id, string Description, decimal Value, TransactionType Type, PersonResponse Person)
{
  public TransactionResponse(TransactionModel transaction)
    : this(transaction.Id, transaction.Description, transaction.Value, transaction.Type, new PersonResponse(transaction.Person)) { }
}