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

        public RecipeController(IRecipe Irecipe, IHostingEnvironment hostingEnvironment, IWebHostEnvironment environment, RecipeDbContext dbContext)
        {
            _irecipe = Irecipe;
            this._hostingEnvironment = hostingEnvironment;
            this._environment = environment;
            this._dbContext = dbContext;
        }

        //get all recipes
        [HttpGet]
        public IActionResult GetAllRecipes()
        {
            var res = this._irecipe.GetRecipes();
            return Ok(res);
        }

        //add recipe
        [HttpPost]
        public IActionResult AddNewRecipe(recipe newRecipecs)
        {
           
            var res = this._irecipe.AddRecipe(newRecipecs);
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
        //search
        [HttpGet("search/{name}")]
        public IActionResult Diltered(string name) 
        { 
            var res=this._irecipe.Filter(name);
            return Ok(res);
        }

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

