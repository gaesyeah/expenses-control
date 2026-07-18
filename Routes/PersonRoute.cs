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

    route.MapPost("", async (PersonRequest req, ExpensesControlContext context) =>
    {
      var person = new PersonModel(req);
      await context.Persons.AddAsync(person);
      await context.SaveChangesAsync();

      var response = new PersonResponse(person);
      return Results.Created($"/{RouteName}/{person.Id}", response);
    });

    route.MapGet("", async (ExpensesControlContext context) =>
    {
      var persons = await context.Persons.Select(person => new PersonResponse(person)).ToListAsync();
      return Results.Ok(persons);
    });

    route.MapGet("{id:guid}", async (Guid id, ExpensesControlContext context) =>
    {
      var person = await context.Persons.FindAsync(id);
      if (person == null) return Results.NotFound();

      var response = new PersonResponse(person);
      return Results.Ok(response);
    });

    route.MapPatch("{id:guid}", async (Guid id, PersonPartialRequest req, ExpensesControlContext context) =>
    {
      var person = await context.Persons.FindAsync(id);
      if (person == null) return Results.NotFound();

      person.ChangePerson(req);
      await context.SaveChangesAsync();

      var response = new PersonResponse(person);
      return Results.Ok(response);
    });

    route.MapDelete("{id:guid}", async (Guid id, ExpensesControlContext context) =>
    {
      var person = await context.Persons.FindAsync(id);
      if (person == null) return Results.NotFound();

      context.Persons.Remove(person);
      await context.SaveChangesAsync();

      return Results.NoContent();
    });
  }
}