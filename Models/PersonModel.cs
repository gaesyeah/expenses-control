namespace Person.Models;

public class PersonModel
{
  // EF Core can't use the public constructor (it takes a DTO, not scalar columns),
  // so this parameterless one lets EF Core build entities from query results
  private PersonModel() { Name = null!; }
  public PersonModel(PersonRequest request)
  {
    Id = Guid.NewGuid();
    Name = request.Name;
    Age = request.Age;
  }

  public Guid Id { get; init; }
  public string Name { get; private set; }
  public int Age { get; private set; }
}