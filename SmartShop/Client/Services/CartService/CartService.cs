
using Blazored.LocalStorage;

namespace SmartShop.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
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
    }
}
