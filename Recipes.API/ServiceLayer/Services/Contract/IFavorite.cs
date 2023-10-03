using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface IFavorite
    {
        public Task<Favorites> Add(Favorites favorites, string userId);

        public Task<List<Favorites>> GetFavorites(string username);

        public string Remove(int Id);
    }
}
