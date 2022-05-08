using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    public class SmartwyreDBContext : DbContext
    {
        public DbSet<Account> Account { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=your-server;Database=SmartwyreDB;Trusted_Connection=True;");
        }
    }
}
