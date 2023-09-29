using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Migrations;
using ServiceLayer.Services.Contract;
using UserManger.Models;
using UserManger.Service;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Iuser _iuser;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmail _iemail;

        public UserController(Iuser iuser ,UserManager<IdentityUser> userManager,IConfiguration configuration, IEmail iemail)
        {
            this._iuser = iuser;
            this._configuration = configuration;
            this._userManager = userManager;
            this._iemail = iemail;
        }
        [HttpPost("Regist")]
        public async Task< IActionResult> regist(User user)
        {
            var res = await _iuser.Register(user);
            return Ok(res);
        }
        [HttpPost("LogIn")]
        public async Task< IActionResult> Log_In(User user)
        {
            var res = await _iuser.LogIn(user);
            var TokenAndEx = res.Split(" ");
            if(TokenAndEx.Length ==4)
            {
                return Ok(new { Token = TokenAndEx[0], Expiration = TokenAndEx[1] });
            }
            return Ok(res);
            
        }
        [HttpGet]
        public IActionResult testEmail()
        {
            var message = new Message(new string[] { "aneven24@gmail.com" }, "Testingg", "<h1>Subscribe to waea</h1>");

            this._iemail.SendEmail(message);
            return Ok("send succ");
        }
        [HttpGet("reset-password")]
        public async Task<IActionResult> resetPassword(string token, string email)
        {
            var newPass = new ResetPassword { Token = token, Email = email };
            return Ok(newPass);
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult>forgetPassword(string username)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var link = "https://localhost:7206/api/User/reset-password?token=" + token + "&email=" + user.Email;
                    var message = new Message(new string[] { user.Email! }, "confirmation email link", link);
                    _iemail.SendEmail(message);
                    return Ok("reset password link sent");
                }
                else
                {
                    return Ok( "user not found");
                }
            }

            catch (Exception ex)
            {
                return Ok( ex.Message);
            }
        }

        ///
        [HttpGet("Confirmation")]
       
        public async Task<IActionResult> Confirmation(string token,string email)
        {
           token= token.Replace(' ', '+');
            var user=await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    
                    return Ok("Confirmation linke sent");
                }
            }
            return Ok("error");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPass = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Passwored);
                if (resetPass.Succeeded)
                {
                    foreach (var error in resetPass.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return Ok("Password has been changeed");
            }
            return Ok("Couldnt send mail to the email");
        }

    }
}
