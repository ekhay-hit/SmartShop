
namespace SmartShop.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        public readonly HttpClient _http;

        public CategoryService(HttpClient http )
        {
            _http= http;
        }


        public List<Category> Categories { get; set; }= new List<Category>();

        public async Task GetCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category");
            if(response != null && response.Data != null) 
            Categories = response.Data;
        }
    }
}
