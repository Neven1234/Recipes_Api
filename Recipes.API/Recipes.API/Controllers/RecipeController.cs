using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using ServiceLayer.Services.Contract;
using System.Net.Http.Headers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Recipes.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipe _irecipe;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _environment;
        private readonly RecipeDbContext _dbContext;
       // private readonly IRateAndReview _IrateAndReview;

        public RecipeController(IRecipe Irecipe, IHostingEnvironment hostingEnvironment, IWebHostEnvironment environment, RecipeDbContext dbContext)
        {
            _irecipe = Irecipe;
            this._hostingEnvironment = hostingEnvironment;
            this._environment = environment;
            this._dbContext = dbContext;
            //this._IrateAndReview = rateAndReview;
        }

        //get all recipes
        [HttpGet]
        public IActionResult GetAllRecipes()
        {
            var res = this._irecipe.GetRecipes();
            return Ok(res);
        }

        //add recipe
        [HttpPost("Add/{username}")]
        public async Task< IActionResult> AddNewRecipe(recipe newRecipecs,string username)
        {
            var res = await _irecipe.AddRecipe(newRecipecs,username);            
            return Ok(res);
        }

        //get a spacific recipe
        [HttpGet("{id:int}")]
        public IActionResult GetSingleRecipe(int id)
        {
            var res = this._irecipe.GetRecipe(id);
            return Ok(res);
        }

        //edite recipy
        [HttpPut("{id:int}")]
        public IActionResult EditeRecipe(int id, recipe recipe)
        {
            var res = this._irecipe.EditeRecipe(id, recipe);
            return Ok(res);
        }

        //delet recipe
        [HttpDelete("{id:int}")]
        public IActionResult DeleteRecipe(int id)
        {
            var res = this._irecipe.RemoveRecipe(id);
            return Ok(res);
        }
        //search buy name
        [HttpGet("search/{name}")]
        public IActionResult searchBuyName(string name) 
        { 
            var res=this._irecipe.Filter(name);
            return Ok(res);
        }
        //search buy ingredients
        [HttpGet("searchIng/{ingredients}")]
        public IActionResult searchBuyIngredients(string ingredients)
        {
            var res = this._irecipe.FilterIngredients(ingredients);
            return Ok(res);
        }

        //Rate And Review

        //[HttpGet("Rate and review")]
        //public IActionResult getReview(int id)
        //{
        //    var res = this._IrateAndReview.Get(id);
        //    return Ok(res);
        //}
        //[HttpPost("Rating/{recipeId:int}/{userName}")]
        //public async Task< IActionResult> AddReview(RateAndReview rateAndReview,int recipeId,string userName)
        //{
        //    var res= await _IrateAndReview.Add(rateAndReview,recipeId,userName);
        //    return Ok(res);
        //}
        //upload image

        [HttpPost("ImageUpload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                var folderName = Path.Combine("Resourcess", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        
    }

}

