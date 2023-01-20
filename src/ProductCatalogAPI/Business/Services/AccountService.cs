using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Data.Repositories;
using ProductCatalogAPI.Models;
using ProductCatalogAPI.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Business.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly JwtOptions _jwtOptions;
       
        public AccountService(IAccountRepository accountRepository, IOptions<JwtOptions> jwtOptions)
        {
            _accountRepository = accountRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var result = await _accountRepository.LoginAsync(email, password);
            if (result.Succeeded)
            {
                var token = GenerateToken(email);
                return new AuthResult { SignInResult = result, Token = token };
            }
            return new AuthResult { SignInResult = result };
        }

        private string GenerateToken(string email)
        {
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task LogoutAsync()
        {
            await _accountRepository.LogoutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
        {
            return await _accountRepository.RegisterAsync(user, password);
        }
    }

    public class AuthResult 
    {
        public SignInResult SignInResult { get; set; }
        public string Token { get; set; }
        public bool Succeeded { get; internal set; }
    }
}
