using ExpensesControl.Data;
using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Routes;

public static class PersonRoute
{
  private const string RouteName = "person";

  public static void PersonRoutes(this WebApplication app)
  {
    var route = app.MapGroup(RouteName);

    route.MapPost("", async (PersonRequest req, ExpansesControlContext context) =>
    {
      var person = new PersonModel(req);
      await context.Persons.AddAsync(person);
      await context.SaveChangesAsync();

      return Results.Created($"/{RouteName}/{person.Id}", person);
    });

    route.MapGet("", async (ExpansesControlContext context) =>
    {
      var persons = await context.Persons.ToListAsync();
      return Results.Ok(persons);
    });

    route.MapGet("{id:guid}", async (Guid id, ExpansesControlContext context) =>
    {
      var person = await context.Persons.FindAsync(id);
      if (person == null) return Results.NotFound();
      return Results.Ok(person);
    });

    route.MapPatch("{id:guid}", async (Guid id, PersonPartialRequest req, ExpansesControlContext context) =>
    {
      var person = await context.Persons.FindAsync(id);
      if (person == null) return Results.NotFound();

      person.ChangePerson(req);
      await context.SaveChangesAsync();

      return Results.Ok(person);
    });

    route.MapDelete("{id:guid}", async (Guid id, ExpansesControlContext context) =>
    {
      var person = await context.Persons.FindAsync(id);
      if (person == null) return Results.NotFound();

      context.Persons.Remove(person);
      await context.SaveChangesAsync();

      return Results.NoContent();
    });
  }
}