
using SmartShop.Server.Data;

namespace SmartShop.Server.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authservice;

        public AddressService(DataContext context, IAuthService authservice)
        {
            _context = context;
            _authservice = authservice;
        }
        public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address)
        {
            // declaire response of type Address
            var response = new ServiceResponse<Address>();
            // get the current user Address
            var dbAddress = (await GetAddress()).Data;
            // if address is null then
            if (dbAddress == null) {
                // get the user id and assing it to address.UserId
                address.UserId = _authservice.GetUserId();
                // add all the other properties to the user address
                _context.Addresses.Add(address);

                response.Data = address;
            }
            else
            {
                //if there is an address, updated with the new received address. 
                dbAddress.FirstName = address.FirstName;
                dbAddress.LastName = address.LastName;
                dbAddress.State = address.State;
                dbAddress.City = address.City;
                dbAddress.Zip = address.Zip;
                dbAddress.Country = address.Country;
                dbAddress.Street = address.Street;
                response.Data = dbAddress;

            }
            //wait untill the changes are saved. 
            await _context.SaveChangesAsync();
            return response;

        }

        public async Task<ServiceResponse<Address>> GetAddress()
        {
            int userId = _authservice.GetUserId();
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.UserId == userId);

            return new ServiceResponse<Address> { Data = address };

        }
    }
}
