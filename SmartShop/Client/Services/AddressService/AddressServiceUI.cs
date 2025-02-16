
namespace SmartShop.Client.Services.AddressService
{
    public class AddressServiceUI : IAddressServiceUI
    {
        private readonly HttpClient _http;

        public AddressServiceUI(HttpClient http)
        {
            _http = http;
        }
        public async Task<Address> AddOrUpdateAddress(Address address)
        {
            var response = await _http.PostAsJsonAsync("api/address", address);

            return response.Content
                .ReadFromJsonAsync<ServiceResponse<Address>>().Result.Data; ;
        }

        public async Task<Address> GetAddress()
        {
            var response = await _http
                .GetFromJsonAsync<ServiceResponse<Address>>("api/address");
            return response.Data;

        }
    }
}
