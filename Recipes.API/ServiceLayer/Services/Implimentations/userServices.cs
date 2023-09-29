using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserManger.Models;
using UserManger.Service;

namespace ServiceLayer.Services.Implimentations
{
    public class userServices : Iuser
    {
       
        private readonly RecipeDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmail _iemail;

        public userServices( UserManager<IdentityUser> userManager, IConfiguration configuration,IEmail email)
        {
           this._userManager = userManager;
           this._configuration = configuration;
           this._iemail = email;
        }

        //reset password
        public async Task<string> ForgertPassword(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if(user != null)
                {
                    var token=await _userManager.GeneratePasswordResetTokenAsync(user);
                    
                    var link = "https://localhost:7206/api/User/reset-password?token=" + token + "&email=" + user.Email;
                    var message = new Message(new string[] { user.Email! }, "confirmation email link", link);
                    _iemail.SendEmail(message);
                    return "reset password link sent";
                }
                else
                {
                    return "user not found";
                }
            }
            
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //login
        public async Task<string> LogIn(User user)
        {
            try
            {
                var userExist = await _userManager.FindByNameAsync(user.UserName);

                if (userExist != null && await _userManager.CheckPasswordAsync(userExist, user.Password))
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };
                    var Roles = await _userManager.GetRolesAsync(userExist);
                    foreach (var role in Roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var jwtToken = getToken(authClaims);
                    var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                    var expiration = DateTime.Now.AddDays(3);
                    return token + " " + expiration;
                }
                else
                {
                    return "Username or Passwored are wrong";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //register
        public async Task<string> Register(User user)
        {
            try
            {
                var userEx = await _userManager.FindByNameAsync(user.UserName);
                var userEx2 = await _userManager.FindByEmailAsync(user.Email);
                if (userEx != null || userEx2!=null)
                {
                    return ("User exist");
                }
                else
                {
                    IdentityUser userr = new()
                    {
                        Email = user.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = user.UserName,
                    };
                    var res = await _userManager.CreateAsync(userr, user.Password);
                    if (res.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(userr, "User");
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(userr);
                        
                        var confirmationLink= "https://localhost:7206/api/User/Confirmation?token=" +token+"&email="+userr.Email;
                        var message = new Message(new string[] { userr.Email! }, "confirmation email link", confirmationLink);
                        _iemail.SendEmail(message);
                        return ("We send you a confirmation mail please confirm it");
                    }
                    else
                    {
                        return ("error");
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }


        }

       


        //helper functions
        private JwtSecurityToken getToken(List<Claim> authClims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: this._configuration["JWT:ValidIssuer"],
                audience: this._configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return token;

        }
       
    }
}
