﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using SmartShop.Client.Services.CartService;
using SmartShop.Server.Services.CartService;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> StoreCartItems(List<CartItem>cartItems)
        {
            var result = await _cartService.StoreCartItems(cartItems);
            return Ok(result);

        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartItem cartItem)
        {
            var result = await _cartService.AddToCart(cartItem);
            return Ok(result);

        }

        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
        {
            var result = await _cartService.GetCartItemsCount();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetDbCartProducts()
        {
            var result = await _cartService.GetDbCartProducts();
            return Ok(result);
        }
        [HttpPut("update-quantity")]
        public async Task<ActionResult<ServiceResponse<bool>>>UpdateQuantity(CartItem cartItem)
        {
            var result = await _cartService.UpdateQuantity(cartItem); 
            return Ok(result);
        }

        [HttpDelete("{productId}/{productTypeId}")] 
        public async Task<ActionResult<ServiceResponse<bool>>>RemoveItemFromCart(int productId, int productTypeid)
        {
            var result = await _cartService.RemoveItemFromCart(productId, productTypeid);
            return Ok(result);
        }
    }
}
