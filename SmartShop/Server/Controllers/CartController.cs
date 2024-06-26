﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using SmartShop.Client.Services.CartService;
using SmartShop.Server.Services.CartService;

namespace SmartShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]

        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartProducts(List<CartItem> cartItems)
        {

            var result = await _cartService.GetCartProducts(cartItems);
            return Ok(result);
        }
    }
}
