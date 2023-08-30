using Expenses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Expenses.Data
{
    public class ExpensesContext : DbContext
    {
        public DbSet<KeyWord> KeyWord { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Establishment> Establishment { get; set; }
        public DbSet<Movement> Movement { get; set; }
        public DbSet<Owner> Owner { get; set; }

        public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options)
        {


        }
    }
}
