using DomainLayer.Models;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface IShoppingList
    {
         Task< string> Add(ShoppingList shoppingList , string UserId);
        IEnumerable<ShoppingList> GetAll(string UserId);
        ShoppingList get(int Id);
        string Edite(int id, ShoppingList shopping);

        string Cleare(string userId);
    }
}
