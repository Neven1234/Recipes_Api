using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implimentations
{
    public class RateAndReviewService : IRateAndReview
    {
        private readonly IRepository<RateAndReview> _Irepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RecipeDbContext _dbContext;

        public RateAndReviewService(IRepository<RateAndReview> repository, UserManager<IdentityUser> userManager,RecipeDbContext dbContext)
        {
            this._Irepository = repository;
            this._userManager = userManager;
            this._dbContext = dbContext;
        }
        public async Task<string> Add(RateAndReview rateAndReview, int recipeId)
        { 
            rateAndReview.RecipeId = recipeId;
            return await _Irepository.Add(rateAndReview);
        }

        public RateAndReview Get(int id)
        {
            return  _Irepository.GetById(id); 
        }

        public List<RateAndReview> GetRateOfRecipe(int RecipeId)
        {
            List<RateAndReview> reviews = new List<RateAndReview>();
            var ReviewsRes = from R in _dbContext.reviews
                             where RecipeId == null ||
                          R.RecipeId.Equals(RecipeId)
                             select new RateAndReview
                             {
                                 Id = R.Id,
                                 Rate = R.Rate,
                                 Review = R.Review,
                                 RecipeId = R.RecipeId,
                                 UserId = R.UserId

                             };
            return ReviewsRes.ToList();
        }
    }
}
