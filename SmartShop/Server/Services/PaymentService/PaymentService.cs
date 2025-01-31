using SmartShop.Server.Services.CartService;
using Stripe.BillingPortal;

namespace SmartShop.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public PaymentService(ICartService cartService,
            IOrderService orderService,
            IAuthService authService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _authService = authService;
        }
        public Task<Session> CreateoutCheckoutSession()
        {
            throw new NotImplementedException();
        }
    }
}
