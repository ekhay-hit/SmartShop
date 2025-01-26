namespace SmartShop.Client.Services.CategoryService
{
    public interface ICategoryServiceUI
    {
       List<Category> Categories { get; set; }
        Task GetCategories();
    }
}
