using Microsoft.EntityFrameworkCore;
using PSSCProject.Data.Models;

namespace PSSCProject.Data
{
    public class ContextForProducts : DbContext
    {
        public ContextForProducts(DbContextOptions<ContextForProducts> options) : base(options)
        {
        }

        public DbSet<ProductsDto> Products { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductsDto>().ToTable("ProductsTable").HasKey(s => s.Id);
        }
    }
}
