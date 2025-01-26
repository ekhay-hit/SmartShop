using SmartShop.Shared;

namespace SmartShop.Client.Services.ProductService
{
    public interface IProductServiceUI
    {
        
        event Action ProductsChanged;
        List<Product> Products{ get; set; }
        string Message { get; set; }
        int CurrentPage {  get; set; }
        int PageCount {  get; set; }

        string LastSearchText {  get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProduct(int productId);
        Task searchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
}
