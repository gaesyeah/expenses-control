using ExpensesControl.Data;
using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Routes;

public static class TransactionRoute
{
  private const string RouteName = "transaction";
  public static void TransactionRoutes(this WebApplication app)
  {
    var route = app.MapGroup(RouteName);

    route.MapPost("", async (TransactionRequest req, ExpansesControlContext context) =>
    {
      var person = await context.Persons.FindAsync(req.PersonId);
      if (person == null) return Results.NotFound();

      var transaction = new TransactionModel(req);
      await context.Transactions.AddAsync(transaction);
      await context.SaveChangesAsync();

      return Results.Created($"/{RouteName}/{transaction.Id}", transaction);
    });

    route.MapGet("", async (ExpansesControlContext context) =>
    {
      var transactions = await context.Transactions.ToListAsync();
      return Results.Ok(transactions);
    });
  }
}