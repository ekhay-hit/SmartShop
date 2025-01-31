
using Microsoft.AspNetCore.Components;
using System;
using System.Text.Json;

namespace SmartShop.Client.Services.OrderServices
{
    public class OrderServiceUI : IOrderServiceUI
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public OrderServiceUI(HttpClient http,
            AuthenticationStateProvider authStateProvider,
            NavigationManager navigationManager)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<List<OrderOverviewResponse>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order");
            return result.Data;
        }

        public async Task<OrderDetailsResponse> GetOrdersDetails(int orderId)
        {
            Console.WriteLine("Fetching order details for ID:", orderId);
            //var response = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"/api/orders/{orderId}");
            try
            {
                
                // Attempt to get the response and deserialize it
                var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"/api/order/{orderId}");
                Console.WriteLine(result);

                if (result == null || result.Data == null)
                {
                    // Handle case where result or data is null
                    Console.WriteLine("No data returned for order details.");
                    throw new Exception("Order details not found.");
                }

                return result.Data;
            }
            catch (HttpRequestException ex)
            {
                // Handle network-related or HTTP errors
                Console.WriteLine($"Network error: {ex.Message}");
                throw new Exception("Error occurred while fetching order details.");
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors
                Console.WriteLine($"Deserialization error: {ex.Message}");
                throw new Exception("Error occurred while processing the response data.");
            }
            catch (Exception ex)
            {
                // Catch any other general errors
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
            //return response?.Data;
        }

        public async Task <string> PlaceOrder()
        {
            if(await IsUserAuthenticated()) 
            {
                var result = await  _http.PostAsync("api/payment/checkout", null);
                var url= await result.Content.ReadAsStringAsync();
                return url;
            }
            else
            {
                return "login";
            }
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}
