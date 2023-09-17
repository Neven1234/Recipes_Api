using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Iuser _iuser;

        public UserController(Iuser iuser)
        {
            this._iuser = iuser;
        }
        [HttpPost("Regist")]
        public IActionResult regist(User user)
        {
            var res = this._iuser.Regist(user);
            return Ok(res);
        }
        [HttpPost("LogIn")]
        public IActionResult Log_In(User user)
        {
            var res = this._iuser.LogIn(user);
            return Ok(res);
        }
    }
}
