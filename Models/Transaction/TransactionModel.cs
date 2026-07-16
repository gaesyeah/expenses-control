namespace ExpensesControl.Models;

public class TransactionModel
{
  public Guid Id { get; init; }
  public string Description { get; private set; }
  public decimal Value { get; private set; }
  public TransactionType Type { get; private set; }
  public Guid PersonId { get; private set; }
  public PersonModel Person { get; private set; } = null!;

  // Construtor exigido pelo EF Core para montar entidades a partir do banco
  // (o construtor público espera um TransactionRequest, não colunas soltas)
  private TransactionModel() { Description = null!; }
  public TransactionModel(TransactionRequest request, PersonModel person)
  {
    if (!Enum.IsDefined(request.Type))
      throw new ArgumentException("Invalid transaction type.", nameof(request));

    if (person.Age < 18 && request.Type != TransactionType.Expense)
      throw new ArgumentException("Minors can only register expenses.", nameof(request));

    Id = Guid.NewGuid();
    Description = request.Description;
    Value = request.Value;
    Type = request.Type;
    PersonId = request.PersonId;
  }
}