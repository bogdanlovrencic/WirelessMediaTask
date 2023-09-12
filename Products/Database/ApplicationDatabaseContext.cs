using Products.Models;
using System.Data.Entity;

namespace Products.Database
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext() : base("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}