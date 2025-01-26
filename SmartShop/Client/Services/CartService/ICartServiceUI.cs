namespace SmartShop.Client.Services.CartService
{
    public interface ICartServiceUI
    {
        event Action OnChange;

        Task AddToCart(CartItem item);
        Task<List<CartProductResponse>> GetCartProducts();
        Task RemoveProductFromCart(int productId, int productTypeId);
        Task UpdateQuantity(CartProductResponse product);
        Task StoreCarItems(bool emptyLocalCart);
        Task GetCartItemsCount();
    }
}
