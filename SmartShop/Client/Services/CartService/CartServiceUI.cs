
using Blazored.LocalStorage;
using SmartShop.Shared;

namespace SmartShop.Client.Services.CartService
{
    public class CartServiceUI : ICartServiceUI
    {

        private readonly ILocalStorageService _localStorage;
        public readonly HttpClient _http;
        private readonly IAuthServiceUI _authService;
       


        public CartServiceUI(ILocalStorageService localStorage, HttpClient http, IAuthServiceUI authService)
        {
            _localStorage = localStorage;
            _http = http;
            _authService = authService;
           
        }

        

        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            // check if the user is authenticated, if use get cart items from database
            if(await _authService.IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/cart/add",cartItem);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    cart = new List<CartItem>();
                }
                var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);
                if (sameItem == null)
                {
                    cart.Add(cartItem);
                }
                else
                {
                    sameItem.Quantity += cartItem.Quantity;
                }

                await _localStorage.SetItemAsync("cart", cart);
            }
            
            await GetCartItemsCount();
        }
   
        public async Task GetCartItemsCount()
        {
            if(await _authService.IsUserAuthenticated())
            {
                var result = await _http.GetFromJsonAsync < ServiceResponse<int> > ("api/cart/count");
                var count = result.Data;
                // storing in local storage
                await _localStorage.SetItemAsync("cartItemsCount", count);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                await _localStorage.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);
            }

            OnChange.Invoke();
        }

        public async Task<List<CartProductResponse>> GetCartProducts()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>("api/cart");
                return response.Data;
            }
            else
            {

                var cartItem = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cartItem != null) return new List<CartProductResponse>();

                var response = await _http.PostAsJsonAsync("api/cart/products", cartItem);
                var cartProducts =
                    await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
                return cartProducts.Data;
            }
        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.DeleteAsync($"api/cart/{productId}/{productTypeId}");
            }
            else
            {

                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    return;
                }
                var cartItem = cart.Find(x => x.ProductId == productId && x.ProductTypeId == productTypeId);

                if (cartItem != null)
                {
                    cart.Remove(cartItem);

                    await _localStorage.SetItemAsync("cart", cart);
                }
            }

                await GetCartItemsCount();
            
           

        }

        public async Task UpdateQuantity(CartProductResponse product)
        {
            if (await _authService.IsUserAuthenticated())
            {
                var request = new CartItem
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    ProductTypeId = product.ProductTypeId
                };
                await _http.PutAsJsonAsync("api/cart/update-quantity", request);

            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    return;
                }
                var cartItem = cart.Find(x => x.ProductId == product.ProductId && x.ProductTypeId == product.ProductTypeId);

                if (cartItem != null)
                {
                    cartItem.Quantity = product.Quantity;
                    await _localStorage.SetItemAsync("cart", cart);

                }
            }
        }

        public async Task StoreCarItems(bool emptyLocalCart =false)
        {
            // get cart item from local storage
            var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if(localCart == null)
            {
                return;
            }

            // passing data to backend to store the items
            await _http.PostAsJsonAsync("api/cart", localCart);
            //impty local storage if requested 
            if (emptyLocalCart)
            {
                await _localStorage.RemoveItemAsync("cart");
            }
        }

       
    }
}
