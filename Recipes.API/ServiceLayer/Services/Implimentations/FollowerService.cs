using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implimentations
{
    public class FollowerService : IFollow
    {
        private readonly IRepository<Follow> _irepository;
        private readonly UserManager<IdentityUser> _userManager;

        public FollowerService(IRepository<Follow> repository , UserManager<IdentityUser> userManager)
        {
            this._irepository = repository;
            this._userManager = userManager;
        }

        public async Task<string> AddFollow(Follow follow, string UserId)
        {
            var user= await _userManager.FindByIdAsync(UserId);
            follow.UserName = user.UserName;
            var res= await _irepository.Add(follow);
            return res;
        }

       
        public IEnumerable<Follow> GetFollows(string UserId)
        {
            var res=this._irepository.GetAll(x=>x.UserToFollowId==UserId);
            return res;
        }
    }
}
