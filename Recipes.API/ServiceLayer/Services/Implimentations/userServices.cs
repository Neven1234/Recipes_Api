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

        public async Task<string> ChangePassword(ChangePassword changePassword, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var IsPasswordCorrect = await _userManager.CheckPasswordAsync(user, changePassword.OldPasswored);
                if(IsPasswordCorrect )
                {
                    if (changePassword.NewPasswored == changePassword.ConfirmPasswored)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var resetPass = await _userManager.ResetPasswordAsync(user, token, changePassword.NewPasswored);
                        if (resetPass.Succeeded)
                        {
                            return ("Password changeed successfully");
                        }
                        return (resetPass.Errors.ToList().ToString());
                    }
                    else
                    {
                        return " New password and confirm password do not match ";
                    }
                }
                else
                {
                    return "current passwored is wrong";
                }
            }
            else
            {
                return "user dosen't exist";
            }
        }

        //confirmation
        public async Task<string> Confirmation(string token, string email)
        {
            token = token.Replace(' ', '+');
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {

                    return ("User Confirmed");
                }
            }
            return ("error");
        }

        //Forget password
        public async Task<string> ForgertPassword(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var link = "https://localhost:7206/api/User/reset-password?token=" + token + "&email=" + user.Email;
                    var message = new Message(new string[] { user.Email! }, "Reset Passwored ", link);
                    _iemail.SendEmail(message);
                    return ("reset password link sent");
                }
                else
                {
                    return ("user not found");
                }
            }

            catch (Exception ex)
            {
                return (ex.Message);
            }
        }


        //get user
        public async Task<User> GetUser(string userId)
        {
            var userTemp=new User();
            var user=await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                userTemp.UserName = user.UserName;
                userTemp.Email = user.Email;
                return userTemp;
            }
            else
            {
                return userTemp;
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
                    if(userExist.EmailConfirmed)
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
                        return "Please conferm your email by click the linke we sent it to you";
                    }
                   
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
                    return ("Username or email is already exist");
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
                        var message = new Message(new string[] { userr.Email! }, "confirmation email link", "if you wnt to confirm your email in recipes websit please click the link \r\n"+confirmationLink);
                        _iemail.SendEmail(message);
                        return ("We send you a confirmation mail please confirm it");
                    }
                    else
                    {
                        return (res.Errors.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }


        }


        //reset password token
        public async Task<string> resetPassword(string token, string email)
        {
            token = token.Replace(' ', '+');
            var newPass = new ResetPassword { Token = token, Email = email };
            return "please copy the next text : \r\n"+token;
        }

        public async Task<string> ResetPassword(ResetPassword resetPassword,string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                resetPassword.Email = user.Email;
                if(resetPassword.ConfirmPasswored==resetPassword.Passwored)
                {
                    var resetPass = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Passwored);
                    if (resetPass.Succeeded)
                    {
                        return ("Password has been changeed");
                    }
                    return resetPass.Errors.ToString();
                }
                else
                {
                    return "New password and confirm password do not match ";
                }
                
            }
            return ("Couldnt send  email");
        }

        //Update user
        public async Task<string> UpdateUser(User user,string username)
        {
            var userr= await _userManager.FindByNameAsync(username);
           
            if (userr!=null)
            {
                userr.UserName= user.UserName;
                userr.Email= user.Email;                
                await _userManager.UpdateAsync(userr);
                return "user Updated succsessfully";
            }
            else
            {
                return "user doesn't exist";
            }
        }

        //get user id
        public async Task<string> UserId(string username)
        {
            var user=await _userManager.FindByNameAsync(username);
            if(user!=null)
            {
                var ID = user.Id;
                return ID;
            }
            return "user not found";
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
