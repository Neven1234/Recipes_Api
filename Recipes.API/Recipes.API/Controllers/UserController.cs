﻿using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Iuser _iuser;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(Iuser iuser ,UserManager<IdentityUser> userManager,IConfiguration configuration)
        {
            this._iuser = iuser;
            this._configuration = configuration;
            this._userManager = userManager;
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
            return Ok(res);
        }
        //[HttpPost("identity")]
        //public async Task<IActionResult> identityReg(User user)
        //{
        //    try
        //    {
        //        var userEx = await _userManager.FindByEmailAsync(user.Email);
        //        if (userEx != null)
        //        {
        //            return Ok("User exist");
        //        }
        //        else
        //        {
        //            IdentityUser userr = new()
        //            {
        //                Email = user.Email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                UserName = user.UserName,
        //            };
        //            var res = await _userManager.CreateAsync(userr, user.Password);
        //            if (res.Succeeded)
        //            {
        //                return Ok("ceated");
        //            }
        //            else
        //            {
        //                return Ok("error");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
           
        //}
    }
}