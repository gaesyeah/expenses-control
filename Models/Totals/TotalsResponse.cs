namespace ExpensesControl.Models;

public record PersonsTotals(string Name, decimal Income, decimal Expense)
{
  public decimal Balance => Income - Expense;
}

public record TotalSummary(decimal Income, decimal Expense, decimal Balance);

public record TotalsResponse(IEnumerable<PersonsTotals> People, TotalSummary Total);