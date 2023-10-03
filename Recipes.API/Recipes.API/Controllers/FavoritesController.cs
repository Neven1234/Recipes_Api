using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavorite _favorite;

        public FavoritesController(IFavorite favorite)
        {
            this._favorite = favorite;
        }

        [HttpGet("Gett/{username}")]
        public async Task<IActionResult> GetFavorites(string username) 
        { 
            var res=await _favorite.GetFavorites(username);
            return Ok(res);
        }

        [HttpPost("Add/{username}")]

        public async Task<IActionResult> AddFavorite(Favorites favorite,string username)
        {
            var res=await _favorite.Add(favorite,username);
            return Ok(res);
        }
        //delet recipe
        [HttpDelete("{id:int}")]
        public IActionResult DeleteRecipe(int id)
        {
            var res=this._favorite.Remove(id);
            return Ok(res);
        }


    }
}
