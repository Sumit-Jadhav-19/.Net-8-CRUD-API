using DotNetCore_CRUD_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore_CRUD_API.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
    }
}
