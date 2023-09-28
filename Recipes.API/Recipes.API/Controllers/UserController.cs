using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            var res = await _iuser.Regist(user);
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

    }
}
