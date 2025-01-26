namespace SmartShop.Client.Services.AuthService
{
    public interface IAuthServiceUI
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>>Login(UserLogin request);
    }
}
