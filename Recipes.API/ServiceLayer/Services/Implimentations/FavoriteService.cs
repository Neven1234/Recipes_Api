using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Interfaces;
using RepositoryLayer;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ServiceLayer.Services.Implimentations
{
    public class FavoriteService : IFavorite
    {
        private readonly IRepository<Favorites> _Irepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RecipeDbContext _dbContext;
        public FavoriteService(IRepository<Favorites> repository, UserManager<IdentityUser> userManager, RecipeDbContext dbContext)
        {
            this._Irepository = repository;
            this._userManager = userManager;
            this._dbContext = dbContext;
        }
        public async Task<Favorites> Add(Favorites favorites, string userId)
        {
          Favorites  empty=new Favorites();
           
            var user = await _userManager.FindByNameAsync(userId);
            favorites.UserId = user.Id;
            this._Irepository.Add(favorites);
            return favorites;
            
           
                
        }


        public async Task<List<Favorites>> GetFavorites(string username)
        {
            var user=await _userManager.FindByNameAsync(username);
            var userId =  user.Id;
            var res = this._Irepository.GetAll(R => R.UserId == user.Id);
            return res.ToList();
        }

        public string Remove(int Id)
        {
            var res = this._Irepository.Remove(Id);
            return res;
        }
    }
}
