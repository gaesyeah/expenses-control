using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Data;

public class ExpensesControlContext : DbContext
{
  public ExpensesControlContext(
    DbContextOptions<ExpensesControlContext> options)
    : base(options)
  { }

  public DbSet<PersonModel> Persons { get; set; }
  public DbSet<TransactionModel> Transactions { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Deletar uma pessoa remove todas as suas transações
    modelBuilder.Entity<TransactionModel>()
        .HasOne(t => t.Person)
        .WithMany(p => p.Transactions)
        .HasForeignKey(t => t.PersonId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}