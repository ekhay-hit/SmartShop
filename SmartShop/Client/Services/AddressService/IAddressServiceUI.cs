namespace SmartShop.Client.Services.AddressService
{
    public interface IAddressServiceUI
    {
        Task<Address> GetAddress();
        Task<Address> AddOrUpdateAddress(Address address);
    }
}
