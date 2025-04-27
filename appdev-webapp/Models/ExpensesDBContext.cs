using Microsoft.EntityFrameworkCore;

namespace appdev_webapp.Models
{
    public class ExpensesDBContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public ExpensesDBContext(DbContextOptions<ExpensesDBContext> options) 
            : base(options)
        { }
    }
}
