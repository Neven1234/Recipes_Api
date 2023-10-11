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

        //Get user
        [HttpGet("GetUser/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var res=await _iuser.GetUser(userId);
            return Ok(res);
        }

        //get user id
        [HttpGet("GetUserID/{username}")]
        public async Task< IActionResult> UserID(string username)
        {
            var res= await _iuser.UserId(username);
            return Ok(res);
        }

        //Update user
        [HttpPut("Update/{userName}")]
        public async Task<IActionResult> UpdateUserInfo(User user,string userName)
        {
            var res=await _iuser.UpdateUser(user,userName);
            return Ok(res);
        }
        //Change password
        [HttpPost("ChangePassword/{username}")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword,string username)
        {
            var res = await _iuser.ChangePassword(changePassword,username);
            return Ok(res);
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> resetPassword(string token, string email)
        {
            var res= await _iuser.resetPassword(token, email);
            return Ok(res);
        }
        [HttpPost("ForgetPassword/{username}")]
        public async Task<IActionResult>forgetPassword(string username)
        {
            var res= await _iuser.ForgertPassword(username);
            return Ok(res);
        }

        ///
        [HttpGet("Confirmation")]
       
        public async Task<IActionResult> Confirmation(string token,string email)
        {
           var res =await _iuser.Confirmation(token, email);
            return Ok(res);
        }

        [HttpPost("reset-password/{username}")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword,string username)
        {
            var res = await _iuser.ResetPassword(resetPassword, username);
            return Ok(res);
        }

    }
}
