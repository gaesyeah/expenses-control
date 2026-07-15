using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Data;

public class PersonContext : DbContext
{
  public DbSet<PersonModel> Persons { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=expensescontrol.sqlite");
    base.OnConfiguring(optionsBuilder);
  }
}