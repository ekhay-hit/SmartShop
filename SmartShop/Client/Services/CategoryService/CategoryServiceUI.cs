
namespace SmartShop.Client.Services.CategoryService
{
    public class CategoryServiceUI : ICategoryServiceUI
    {
        public readonly HttpClient _http;

        public CategoryServiceUI(HttpClient http )
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
