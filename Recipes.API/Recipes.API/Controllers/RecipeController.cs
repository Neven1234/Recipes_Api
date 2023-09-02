using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipe _irecipe;

        public RecipeController(IRecipe Irecipe)
        {
            _irecipe = Irecipe;
        }

        //get all recipes
        [HttpGet]
        public IActionResult GetAllRecipes()
        {
            var res=this._irecipe.GetRecipes();
            return Ok(res);
        }

        //add recipe
        [HttpPost]
        public IActionResult AddNewRecipe(recipe recipe)
        {
            var res = this._irecipe.AddRecipe(recipe);
            return Ok(res);
        }

        //get a spacific recipe
        [HttpGet("{id:int}")]
        public IActionResult  GetSingleRecipe(int id)
        {
            var res=this._irecipe.GetRecipe(id);
            return Ok(res);
        }

        //edite recipy
        [HttpPut("{id:int}")]
        public IActionResult EditeRecipe(int id , recipe recipe)
        {
            var res=this._irecipe.EditeRecipe(id, recipe);
            return Ok(res);
        }

        //delet recipe
        [HttpDelete]
        public IActionResult DeleteRecipe(int id)
        {
            var res=this._irecipe.RemoveRecipe(id);
            return Ok(res);
        }
    }
}
