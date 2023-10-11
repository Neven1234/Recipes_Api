using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implimentations
{
    public class CommentService : IComment
    {
        private readonly IRepository<Comment> _irepository;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentService(IRepository<Comment> repository, UserManager<IdentityUser> userManager)
        {
            this._irepository = repository;
            this._userManager = userManager;
        }

        public async Task<string> AddComment(Comment comment, string UserId)
        {
            var user=await _userManager.FindByIdAsync(UserId);
            comment.UserName = user.UserName;
            var res= await _irepository.Add(comment);
            return res;
        }

        public List<Comment> GetComments(int RecipeId)
        {
            var res = this._irepository.GetAll(x => x.RecipeId == RecipeId);

            return res.ToList();
        }
    }
}
