using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowAndCommentController : ControllerBase
    {
        private readonly IFollow _ifollow;
        private readonly IComment _icomment;

        public FollowAndCommentController(IFollow follow,IComment comment)
        {
            this._ifollow = follow;
            this._icomment = comment;
        }

        ////Follow
        [HttpPost("Follow/{UserId}")]
        public IActionResult Add (Follow follow ,string UserId) 
        {
            var res=this._ifollow.AddFollow(follow,UserId);
            return Ok(res);
        }
        [HttpGet("GetFollowe/{UserId}")]
        public IActionResult GetFollowers (string UserId)
        {
            var res= this._ifollow.GetFollows(UserId);
            return Ok(res);
        }

        ///Comments

        [HttpPost("Comment/{UserId}")]
        public IActionResult AddComment(Comment comment,string UserId)
        {
            var res = this._icomment.AddComment(comment,UserId);
            return Ok(res);
        }
        [HttpGet("GetComments/{RecipeId:int}")]
        public async Task< IActionResult >GetComments(int RecipeId)
        {
            var res=  _icomment.GetComments(RecipeId);
            return Ok(res);
        }
    }
}
