using Stripe.BillingPortal;

namespace SmartShop.Server.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<Session> CreateoutCheckoutSession();
    }
}
