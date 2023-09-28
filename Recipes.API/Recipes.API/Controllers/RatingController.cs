using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRateAndReview _Irate;

        public RatingController(IRateAndReview rate)
        {
            this._Irate = rate;
        }
        [HttpGet("Rate and review")]
        public IActionResult getReview(int id)
        {
            var res = this._Irate.Get(id);
            return Ok(res);
        }
        [HttpPost("Rating/{recipeId:int}")]
        public async Task<IActionResult> AddReview(RateAndReview rateAndReview, int recipeId)
        {
            var res = await _Irate.Add(rateAndReview, recipeId);
            return Ok(res);
        }
        [HttpGet("{recipeId:int}")]
        public IActionResult GetAllReviews(int recipeId)
        {
            var res= this._Irate.GetRateOfRecipe(recipeId);
            return Ok(res);
        }
    }
}
