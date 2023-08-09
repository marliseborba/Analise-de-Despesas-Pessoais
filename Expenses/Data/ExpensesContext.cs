using Expenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Data
{
    public class ExpensesContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Establishment> Establishment { get; set; }
        public DbSet<Movement> Movement { get; set; }

        public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options)
        {
        }

        /*
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your connection string.
            var connectionString = "server=localhost;user=root1;password=1234;database=myNewDatabase";

            var serverVersion = new MySqlServerVersion(new Version(10, 4, 27));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
         */
    }
}
