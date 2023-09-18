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
        private readonly DbSet<T> entities;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public UserRepository(RecipeDbContext dbContext, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this.entities = this._dbContext.Set<T>();
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public  string LogIn(T user)
        {
            try 
            {
                var userr = this.entities.FirstOrDefault(x => x.Password == user.Password && x.UserName == user.UserName);
                if (userr != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };
                    var jwtToken = getToken(authClaims);
                    var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                    var expiration = jwtToken.ValidTo;
                    userr.Token = token;
                    this._dbContext.Update(userr);
                    this._dbContext.SaveChanges();
                    return  token ;
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
                var userr = this.entities.FirstOrDefault(X => X.UserName == user.UserName);
                if (userr != null)
                {
                    return "user exist";
                }
                IdentityUser us = new()
                {
                    Email = user.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = user.UserName

                };
                var res = this._userManager.CreateAsync(us, user.Password);
                if (res.IsCompletedSuccessfully)
                {                  
                    this.entities.Add(user);
                    this._dbContext.SaveChanges();
                    return "User Created Successfully";
                }
                else
                {
                    return "error";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
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
