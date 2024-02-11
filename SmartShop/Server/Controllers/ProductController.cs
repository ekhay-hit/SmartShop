using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartShop.Server.Data;

namespace SmartShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


       private readonly IProductService _productService;
        public ProductController(IProductService productService) {
         
            _productService = productService;
        }

        public IProductService ProductService { get; }

        [HttpGet]

        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var result = await _productService.GetProductsAsync();
            
            return Ok(result);
        }


        // getting a single product 
        [HttpGet("{productId}")]

        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
        {
            var result = await _productService.GetProductAsync(productId);

            return Ok(result);
        }

    }
}
