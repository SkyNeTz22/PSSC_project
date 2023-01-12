using LanguageExt;
using Microsoft.EntityFrameworkCore;
using PSSCProject.Domain.Models;
using PSSCProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSCProject.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsContext dbContext;
        public ProductsRepository(ProductsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        TryAsync<List<RetrievedProductsList>> IProductsRepository.TryGetExistingProductsByCategory(string CategoryName)
        {
            return async () => (await (
                    from prodsTable in dbContext.ProductsTable
                    where prodsTable.Category == CategoryName
                    select new { prodsTable.Category, prodsTable.Name, prodsTable.Price, prodsTable.Stoc })
                    .AsNoTracking()
                    .ToListAsync())
                    .Select(result => new RetrievedProductsList(
                                            new Category(result.Category),
                                            Name: result.Name,
                                            Price: result.Price,
                                            Stoc: result.Stoc))
                    .ToList();
                    
        }

        TryAsync<List<RetrievedProductsList>> IProductsRepository.TryGetByProductName(string productName)
        {
            return async () => (await (
                    from prodsTable in dbContext.ProductsTable
                    where prodsTable.Name == productName
                    select new { prodsTable.Category, prodsTable.Name, prodsTable.Price, prodsTable.Stoc })
                    .AsNoTracking()
                    .ToListAsync())
                    .Select(result => new RetrievedProductsList(
                                            new Category(result.Category),
                                            Name: result.Name,
                                            Price: result.Price,
                                            Stoc: result.Stoc))
                    .ToList();

        }
    }
}
