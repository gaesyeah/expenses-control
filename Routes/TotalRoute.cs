using ExpensesControl.Data;
using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Routes;

public static class TotalRoute
{
  public static void TotalRoutes(this WebApplication app)
  {
    var route = app.MapGroup("totals");

    // A soma de receitas/despesas e a contagem de transações são calculadas
    // no próprio banco (o EF Core traduz isso para SQL), evitando trazer
    // todas as transações para a memória só para somar.
    route.MapGet("", async (ExpensesControlContext context) =>
    {
      var peopleTotals = await context.Persons
        .Select(p => new PersonTotals(
          p.Name,
          p.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value),
          p.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Value),
          p.Transactions.Count))
        .ToListAsync();

      var response = new TotalsResponse(peopleTotals);
      return Results.Ok(response);
    });
  }
}