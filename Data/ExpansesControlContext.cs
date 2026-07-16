using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Data;

public class ExpensesControlContext : DbContext
{
  public DbSet<PersonModel> Persons { get; set; }
  public DbSet<TransactionModel> Transactions { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=expensescontrol.sqlite");
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<TransactionModel>()
        .HasOne(t => t.Person)
        .WithMany(p => p.Transactions)
        .HasForeignKey(t => t.PersonId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}