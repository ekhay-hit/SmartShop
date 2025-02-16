using SmartShop.Server.Services.CartService;
using Stripe;
using Stripe.Checkout;


namespace SmartShop.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        const string secret = "whsec_406169c308ada8fccc311a43e32bd287df2c1166bb45cd9a7c5946659213127f";

        public PaymentService(ICartService cartService,
            IOrderService orderService,
            IAuthService authService)
        {
            StripeConfiguration.ApiKey = "sk_test_51QnPTs2NcxKNi27ddrkLRMNDX5SFmZt785MpF0EonHZw5jSPlSqcNgD2T3pPyXoplQqyVCQNNMpThRgnUlZoKitr00lvNos3w2";
            _cartService = cartService;
            _orderService = orderService;
            _authService = authService;
        }

        public async Task<Session> CreateoutCheckoutSession()
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            var lineItems = new List<SessionLineItemOptions>();
            products.ForEach(product => lineItems.Add(new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title,
                        Images = new List<string> { product.ImageUrl }
                    }

                },
                Quantity = product.Quantity,
            }));
            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "US"}
                }

                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:5001/order-success",
                CancelUrl = "https://localhost:5001/cart"

            };
            var service = new SessionService();
            Session session =  service.Create(options);
            return session;

        }

        public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request)
        {
           var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    request.Headers["Stripe-Signature"],
                    secret
                    );

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmail(session.CustomerEmail);

                    await _orderService.PlaceOrder(user.Id);
                   
                }
                return new ServiceResponse<bool> { Data = true };
            }
            catch(StripeException e)
            {
                return new ServiceResponse<bool> { Data = false, success = false, Message =e.Message };
            };
        }
    }
    }
