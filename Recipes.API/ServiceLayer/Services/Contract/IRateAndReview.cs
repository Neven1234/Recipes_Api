using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface IRateAndReview
    {
        public Task<string> Add(RateAndReview rateAndReview,int recipeId);

        public RateAndReview Get(int id);

        public List< RateAndReview> GetRateOfRecipe(int RecipeId);

    }
}
