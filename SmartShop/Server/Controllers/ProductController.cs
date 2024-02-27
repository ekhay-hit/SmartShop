using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SmartShop.Server.Data;
using SmartShop.Shared;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

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

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>>GetProductsByCategory(string categoryUrl)
        {
            var result= await _productService.GetProductsByCategory(categoryUrl);

            return Ok(result);
        }

        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts(string searchText, int page=1)
        {
            var result = await _productService.SearchProducts(searchText, page);

            return Ok(result);
        }


        [HttpGet("searchsuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProdutSearchSuggestions(string searchText)
        {
            var result = await _productService.GetProductSearchSuggestions(searchText);

            return Ok(result);
        }


        [HttpGet("Featured")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>>GetFeaturedProducts()
     
        {
            var result = await _productService.GetFeaturedProducts();


            return Ok(result);
        }

    }
}
