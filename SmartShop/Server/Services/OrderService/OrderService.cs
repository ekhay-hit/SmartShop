
using SmartShop.Client.Services.CartService;
using SmartShop.Server.Data;

namespace SmartShop.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(DataContext context,
                ICartService cartService,
                IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<ServiceResponse<bool>> PlaceOrder()
        
        {
            throw new NotImplementedException();
        }
    }
}
