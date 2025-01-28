namespace SmartShop.Client.Services.OrderServices
{
    public interface IOrderServiceUI
    {
        Task PlaceOrder();
        Task<List<OrderOverviewResponse>> GetOrders();
    }
}
