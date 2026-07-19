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

    route.MapPost("", async (TransactionRequest req, ExpensesControlContext context) =>
    {
      var person = await context.Persons.FindAsync(req.PersonId);
      if (person == null) return Results.NotFound();

      var transaction = new TransactionModel(req, person);
      await context.Transactions.AddAsync(transaction);
      await context.SaveChangesAsync();

      var response = new TransactionResponse(transaction);
      return Results.Created($"/{RouteName}/{transaction.Id}", response);
    });

    route.MapGet("", async (ExpensesControlContext context) =>
    {
      var transactions = await context.Transactions.Include(t => t.Person).Select(transaction => new TransactionResponse(transaction)).ToListAsync();
      return Results.Ok(transactions);
    });
  }
}