using LanguageExt;
using System.Collections.Generic;

namespace PSSCProject.Domain.Repositories
{
	public interface IProductsRepository
	{
		TryAsync<List<ProductsInDb>> TryGetExistingProductsByCategory();

		TryAsync<Unit> TrySaveProducts(CurrentProducts products);
	}
}
