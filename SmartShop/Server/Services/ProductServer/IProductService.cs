using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace SmartShop.Server.Services.ProductServer
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
    }
}
