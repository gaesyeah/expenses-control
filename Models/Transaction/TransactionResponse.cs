namespace ExpensesControl.Models;

public record TransactionResponse(Guid Id, string Description, decimal Value, TransactionType Type, Guid PersonId);