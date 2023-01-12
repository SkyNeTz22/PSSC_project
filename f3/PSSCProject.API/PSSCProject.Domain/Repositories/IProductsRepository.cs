using LanguageExt;
using PSSCProject.Domain.Models;
namespace PSSCProject.Domain.Repositories
{
    public interface IProductsRepository
    {
        TryAsync<List<RetrievedProductsList>> TryGetExistingProductsByCategory(string CategoryName);
        TryAsync<List<RetrievedProductsList>> TryGetByProductName(string ProductName);
    }
   
}
