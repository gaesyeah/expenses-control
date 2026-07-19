namespace ExpensesControl.Models;

public record Totals(decimal Income, decimal Expense, int Count)
{
  public decimal Balance => Income - Expense;
}

// Herda de Totals porque "totais de uma pessoa" é, na prática, um resumo de totais com nome a mais.
public record PersonTotals(string Name, decimal Income, decimal Expense, int Count)
  : Totals(Income, Expense, Count);

public record TotalsResponse(IEnumerable<PersonTotals> Persons, Totals Total)
{
  // Soma os totais já calculados de cada pessoa (não precisa de banco aqui,
  // é só agregação em memória).
  public TotalsResponse(IEnumerable<PersonTotals> persons)
    : this(persons, new Totals(persons.Sum(p => p.Income), persons.Sum(p => p.Expense), persons.Count())) { }
}