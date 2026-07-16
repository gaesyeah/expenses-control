using ExpensesControl.Data;
using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Routes;

public static class TotalRoute
{
  public static void TotalRoutes(this WebApplication app)
  {
    var route = app.MapGroup("totals");

    route.MapGet("", async (ExpensesControlContext context) =>
    {
      var peopleTotals = await context.Persons
              .Select(p => new PersonsTotals(
                  p.Name,
                  p.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value),
                  p.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Value)))
              .ToListAsync();

      var totalIncome = peopleTotals.Sum(p => p.Income);
      var totalExpense = peopleTotals.Sum(p => p.Expense);

      var response = new TotalsResponse(
              peopleTotals,
              new TotalSummary(totalIncome, totalExpense, totalIncome - totalExpense));

      return Results.Ok(response);
    });
  }
}