using ExpensesControl.Data;
using ExpensesControl.Models;

namespace ExpensesControl.Routes;

public static class PersonRoute
{
  private const string RouteName = "person";

  public static void PersonRoutes(this WebApplication app)
  {

    var route = app.MapGroup(RouteName);

    route.MapPost("", async (PersonRequest req, PersonContext ctx) =>
    {
      var person = new PersonModel(req);
      await ctx.Persons.AddAsync(person);
      await ctx.SaveChangesAsync();

      return Results.Created($"/{RouteName}/{person.Id}", person);
    });
  }
}