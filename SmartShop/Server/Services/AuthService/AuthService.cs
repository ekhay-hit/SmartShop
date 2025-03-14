﻿
using Microsoft.IdentityModel.Tokens;
using SmartShop.Server.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SmartShop.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context , IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.success = false;
                response.Message = "User not found";
            }

            else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt )) 
            {
                response.success = false;
                response.Message = " Wrong password entered";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            

                     return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    success = false,
                    Message = "User already exists"


                };

            }
            CreatePasswordHash(password, out byte[]passwordHash, out byte[]passwordsalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordsalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            return new ServiceResponse<int> { Data = user.Id , Message= "Registration is successful!"};
        }




            public async Task<bool> UserExists(string email)
            {
                if (await _context.Users.AnyAsync(user => user.Email.ToLower() == email.ToLower()))
                {
                    return true;
                }
                return false;
            }






        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        { 
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash= hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }




        private bool VerifyPasswordHash(string password, byte[]passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool> { 
                    success = false,
                    Message="User not found."
                };


            }
            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> {
                Data = true,
                Message = "Password has been changed."
            };


        }
            public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        public async Task<User> GetUserByEmail(string email)
        {
           return await _context.Users.FirstOrDefaultAsync( u => u.Email.Equals( email));
        }
    }
}
