using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.implimentation
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        private readonly RecipeDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DbSet<T> entities;
        private readonly IConfiguration _configuration;

        public UserRepository(RecipeDbContext dbContext,UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this.entities = this._dbContext.Set<T>();
            this._configuration = configuration;
        }

        public async Task< string> LogIn(T user)
        {
            try 
            {
                var userExist = await _userManager.FindByNameAsync(user.UserName);
                
                if (userExist!=null && await _userManager.CheckPasswordAsync(userExist,user.Password))
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };
                    var Roles = await _userManager.GetRolesAsync(userExist);
                    foreach(var role in Roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var jwtToken = getToken(authClaims);
                    var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                    var expiration = jwtToken.ValidTo;
                    return token;
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

        public async Task<string> Regist(T user)
        {
            try
            {
                var userEx = await _userManager.FindByEmailAsync(user.Email);
                if (userEx != null)
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
                       await  _userManager.AddToRoleAsync(userr, "User");
                        return ("ceated");
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
