namespace SmartShop.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder();
        Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders();
        Task<ServiceResponse<List<OrderDetailsResponse>>> GetOrderDetails(int orderId);
    }
}
