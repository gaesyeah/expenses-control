namespace ExpensesControl.Models;

public class PersonModel
{
  public Guid Id { get; init; }
  public string Name { get; private set; }
  public int Age { get; private set; }
  public ICollection<TransactionModel> Transactions { get; private set; } = [];

  // Construtor exigido pelo EF Core para montar entidades a partir do banco
  // (o construtor público espera um PersonRequest, não colunas soltas)
  private PersonModel() { Name = null!; }
  public PersonModel(PersonRequest request)
  {
    ValidateAge(request.Age);
    ValidateName(request.Name);

    Id = Guid.NewGuid();
    Name = request.Name;
    Age = request.Age;
  }

  public void ChangePerson(PersonPartialRequest request)
  {
    if (request.Name is not null)
    {
      ValidateName(request.Name);
      Name = request.Name;
    }

    if (request.Age is not null)
    {
      ValidateAge(request.Age.Value);
      Age = request.Age.Value;
    }
  }

  private static void ValidateName(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Name cannot be empty.", nameof(name));
  }

  private static void ValidateAge(int age)
  {
    if (age < 0)
      throw new ArgumentException("Age cannot be negative.", nameof(age));
  }
}