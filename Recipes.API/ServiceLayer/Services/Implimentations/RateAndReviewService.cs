﻿using DomainLayer.Models;
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
            var user = await _userManager.FindByIdAsync(rateAndReview.UserId);
            rateAndReview.UserName = user.UserName;
           // rateAndReview.UserId =  user.Id;
            return await _Irepository.Add(rateAndReview);
        }

        public RateAndReview Get(int id)
        {
            return  _Irepository.GetById(id); 
        }

        public List<RateAndReview> GetRateOfRecipe(int RecipeId)
        {
            var res=this._Irepository.GetAll(x=>x.RecipeId== RecipeId);
           
            return res.ToList();
        }
    }
}
