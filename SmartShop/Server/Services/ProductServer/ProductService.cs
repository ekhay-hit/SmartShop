﻿using SmartShop.Server.Data;

namespace SmartShop.Server.Services.ProductServer
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product= await _context.Products.FindAsync(productId);
            if (product == null)
            {
                response.success = false;
                response.Message = "Sorry, we could not find the product";
            }
            else
            {
                response.Data = product;
            }
            return response;
           
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.ToListAsync()
            };
            return response;

        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data= await _context.Products.Where(p=> p.Category.Url.ToLower().Equals(categoryUrl.ToLower())).ToListAsync()
            };

            return response;
        }
    }
}