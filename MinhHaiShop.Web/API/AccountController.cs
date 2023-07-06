using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhHaiShop.Model.Models;
using MinhHaiShop.Service;
using MinhHaiShop.Web.Models;

namespace MinhHaiShop.Web.API
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUp signUpViewModel)
        {
            var result = await _accountService.SignupAsync(signUpViewModel);
            if (result.Succeeded)   
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(SignIn signInViewModel)
        {
            var result = await _accountService.SignInAsync(signInViewModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
