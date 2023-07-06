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
        public Task<string> SignInAsync(SignIn signIn);
        public Task<IdentityResult> SignupAsync(SignUp signUp);
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

        public async Task<string> SignInAsync(SignIn signIn)
        {
            var result = await _signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, false, false);

            if (!result.Succeeded)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, signIn.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignupAsync(SignUp signUp)
        {
            var user = new ApplicationUser
            {
                FullName = signUp.FullName,
                Email = signUp.Email,
                Address = signUp.Address,
                BirthDay = signUp.BirthDay,
                UserName = signUp.Email,
                Password = signUp.Password,

            };
            return await _userInManager.CreateAsync(user, signUp.Password);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

    }
}
