global using System.Net.Http.Json;
global using SmartShop.Shared;
global using SmartShop.Client.Services.ProductService;
global using SmartShop.Client.Services.CategoryService;
global using SmartShop.Client.Services.CartService;
global using SmartShop.Client.Services.AuthService;
global using SmartShop.Client.Services.OrderServices;
global using SmartShop.Client.Services.AddressService;
global using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartShop.Client;
using Blazored.LocalStorage;




var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductServiceUI, ProductServiceUI>();
builder.Services.AddScoped<ICategoryServiceUI, CategoryServiceUI>();
builder.Services.AddScoped<ICartServiceUI, CartServiceUI>();
builder.Services.AddScoped<IAuthServiceUI, AuthServiceUI>();
builder.Services.AddScoped<IOrderServiceUI, OrderServiceUI>();
builder.Services.AddScoped<IAddressServiceUI, AddressServiceUI>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
await builder.Build().RunAsync();
