namespace ExpensesControl.Models;

public record PersonResponse(Guid Id, string Name, int Age)
{
  public PersonResponse(PersonModel person)
    : this(person.Id, person.Name, person.Age) { }
}