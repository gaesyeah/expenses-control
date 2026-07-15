namespace ExpensesControl.Models;

public record TransactionRequest(string Description, decimal Value, TransactionType Type, Guid PersonId);
