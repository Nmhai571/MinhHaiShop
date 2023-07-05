using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Model.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinhHaiShop.Service
{
    public interface IAccountService
    {
        public Task<string> SignInAsync(ApplicationUser applicationUser);
        public Task<IdentityResult> SignupAsync(ApplicationUser applicationUser);
        void SaveChanges();
    }
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userInManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IDbFactory dbFactory)
        {
            _signInManager = signInManager;
            _userInManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> SignInAsync(ApplicationUser applicationUser)
        {
            var result = await _signInManager.PasswordSignInAsync
                (applicationUser.UserName, applicationUser.Password, false, false);
            if (result.Succeeded)
            {
                return string.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidIssuer"],
                    expires: DateTime.Now.AddMinutes(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignupAsync(ApplicationUser applicationUser)
        {
            var user = new ApplicationUser
            {
                FullName = applicationUser.FullName,
                Email = applicationUser.Email,
                Address = applicationUser.Address,
                BirthDay = applicationUser.BirthDay,
                UserName = applicationUser.UserName

            };
            return await _userInManager.CreateAsync(user, applicationUser.Password);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

    }
}
