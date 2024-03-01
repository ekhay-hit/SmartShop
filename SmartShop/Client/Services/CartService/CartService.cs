﻿
using Blazored.LocalStorage;

namespace SmartShop.Client.Services.CartService
{
    public class CartService : ICartService
    {

        private readonly ILocalStorageService _localStorage;
        public HttpClient _http { get; }

        public CartService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        

        public event Action OnChange;

        public async Task AddToCart(CartItem item)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart=new List<CartItem>();
            }
                cart.Add(item);

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if(cart == null)
            {
                cart=new List<CartItem>();
            }

                return cart;
        }

        public async Task<List<CartProductResponse>> GetCartProducts()
        {
            var cartItem = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            var response = await _http.PostAsJsonAsync("api/cart/products",cartItem);
            var cartProducts =
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
            return cartProducts.Data;
        }
    }
}