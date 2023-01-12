using Microsoft.AspNetCore.Mvc;
using PSSCProject.Domain.Repositories;

namespace PSSCProject.API.Controllers
{

    [ApiController]
    [Route("[controller]/")]
    public class ProductsController : ControllerBase
    {
        //public ProductsDto Get { [FromUri] ProductsDto Category }

        private ILogger<ProductsController> logger;
        public ProductsController(ILogger<ProductsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("/category", Name = "GetProductsByCategory")]
        public async Task<IActionResult> GetAllProductsByCategory(string category, [FromServices] IProductsRepository productsRepository) =>
           await productsRepository.TryGetExistingProductsByCategory(category).Match(
              Succ: GetAllProductsHandleSuccess,
              Fail: GetAllProductsHandleError
           );

       
        private ObjectResult GetAllProductsHandleError(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
        }

        private OkObjectResult GetAllProductsHandleSuccess(List<Domain.Models.RetrievedProductsList> products) =>
        Ok(products.Select(products => new
        {
            products.Category,
            products.Name,
            products.Price,
            products.Stoc
        }));

        [HttpGet("/productName", Name = "GetProductsByProductName")]
        public async Task<IActionResult> GetAllProductsByName(string productName, [FromServices] IProductsRepository productsRepository) =>
         await productsRepository.TryGetByProductName(productName).Match(
            Succ: GetAllProductsHandleSuccess,
            Fail: GetAllProductsHandleError
         );
    }
}