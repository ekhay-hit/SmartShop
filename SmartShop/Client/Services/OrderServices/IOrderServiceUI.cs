namespace SmartShop.Client.Services.OrderServices
{
    public interface IOrderServiceUI
    {
        Task <string> PlaceOrder();
        Task<List<OrderOverviewResponse>> GetOrders();
        Task<OrderDetailsResponse> GetOrdersDetails(int orderId);
    }
}
