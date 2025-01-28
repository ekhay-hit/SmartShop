
using SmartShop.Server.Services.CartService;
using SmartShop.Server.Data;
using System.Security.Claims;

namespace SmartShop.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
    

        public OrderService(DataContext context,
                ICartService cartService,
                IAuthService authService)
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
           
        }

        public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders()
        {
           var response = new ServiceResponse<List<OrderOverviewResponse>>();

            var userId = _authService.GetUserId();
            Console.WriteLine($"UserId*******************************************************: {userId}");

            var orders = await _context.Orders.
                Include(o => o.OrderItems)
                .ThenInclude(oi=> oi.Product)
                .Where(o=>o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            Console.WriteLine($"Orders Count****************************************: {orders.Count}");
            var orderResponse = new List<OrderOverviewResponse>();
            //orders.ForEach(o => orderResponse.Add(new OrderOverviewResponse
            //{
            //    Id = o.Id,
            //    OrderDate = o.OrderDate,
            //    TotalPrice = o.TotalPrice,
            //    Product = o.OrderItems.Count > 1 ?
            //    $"{o.OrderItems.First().Product.Title} and" + 
            //    $"{o.OrderItems.Count - 1} more..." :
            //    o.OrderItems.First().Product.Title,
            //    ProductImageUrl = o.OrderItems.First().Product.ImageUrl
            //}));

            orders.ForEach(o =>
            {
                // Check if there are any OrderItems in the order
                var firstOrderItem = o.OrderItems.FirstOrDefault();

                // If there are no order items, you can skip this order or handle it as needed.
                if (firstOrderItem != null)
                {
                    orderResponse.Add(new OrderOverviewResponse
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        TotalPrice = o.TotalPrice,
                        Product = o.OrderItems.Count > 1 ?
                            $"{firstOrderItem.Product.Title} and {o.OrderItems.Count - 1} more..." :
                            firstOrderItem.Product.Title,
                        ProductImageUrl = firstOrderItem.Product.ImageUrl
                    });
                }
                else
                {
                    // Optionally log or handle the case where an order has no items
                    Console.WriteLine($"Order ID {o.Id} has no items.");
                }
            });

            Console.WriteLine($"Orders Processed: {orderResponse.Count}");


            Console.WriteLine(orders.Count);
            response.Data = orderResponse;

            return response;

        }

        public async Task<ServiceResponse<bool>> PlaceOrder()
        
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            decimal totalPrice = 0;
            products.ForEach(product => totalPrice += product.Price * product.Quantity);

            var orderItems = new List<OrderItem>();
            products.ForEach(product => orderItems.Add(new OrderItem { 
                ProductId = product.ProductId,
                ProductTypeId = product.ProductTypeId,
                Quantity = product.Quantity,
                TotalPrice = product.Price * product.Quantity
            }));

            var order = new Order { 
                UserId = _authService.GetUserId(),
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems
            };
            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(_context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()));
            await _context.SaveChangesAsync();
           

            return new ServiceResponse<bool> { Data = true };


        }
    }
}
