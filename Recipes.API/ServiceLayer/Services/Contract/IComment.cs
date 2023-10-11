using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface IComment
    {
        Task<string> AddComment(Comment comment, string UserId);
        public List<Comment> GetComments(int RecipeId);
    }
}
