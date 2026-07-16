namespace ExpensesControl.Models;

public record Totals(decimal Income, decimal Expense, int Count)
{
  public decimal Balance => Income - Expense;
}

public record PersonTotals(string Name, decimal Income, decimal Expense, int Count)
    : Totals(Income, Expense, Count);

public record TotalsResponse(IEnumerable<PersonTotals> People, Totals Total)
{
  public TotalsResponse(IEnumerable<PersonTotals> people)
    : this(people, new Totals(people.Sum(p => p.Income), people.Sum(p => p.Expense), people.Count())) { }
}