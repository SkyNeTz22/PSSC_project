using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSSCProject.Data.Models;

namespace PSSCProject.Data
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
        }
        public DbSet<ProductsDto> ProductsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductsDto>().ToTable("ProductsTable").HasKey(prodsTable => prodsTable.Id);
        }
    }
}
