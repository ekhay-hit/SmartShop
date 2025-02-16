using Stripe.Checkout;

namespace SmartShop.Server.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<Session> CreateoutCheckoutSession();
        Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request);
    }
}
