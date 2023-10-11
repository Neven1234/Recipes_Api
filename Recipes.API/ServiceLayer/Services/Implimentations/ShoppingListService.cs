using DomainLayer.Models;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implimentations
{
    public class ShoppingListService : IShoppingList
    {
        private readonly IRepository<ShoppingList> _irepository;

        public ShoppingListService(IRepository<ShoppingList> repository)
        {
            this._irepository = repository;
        }

        //add
        public async Task< string> Add(ShoppingList shoppingList, string UserId)
        {
            shoppingList.UserId = UserId;
            var res =await _irepository.Add(shoppingList);
            return res;
        }

        //Cleare the shopping list
        public string Cleare(string userId)
        {
            var res=this._irepository.GetAll(x=>x.UserId == userId);
            foreach(var item in res)
            {
                this._irepository.Remove(item.Id);
            }
            return "The list has been cleared successfully ";
        }

        //edite
        public string Edite(int id, ShoppingList shopping)
        {
            var res=this._irepository.Edite(id, shopping);
            return res;
        }

        //get on item
        public ShoppingList get(int Id)
        {
            var res =this._irepository.GetById(Id);
            return res;
        }

        //get all shopping list of the user
        public IEnumerable<ShoppingList> GetAll(string UserId)
        {
            var res= this._irepository.GetAll(x=>x.UserId==UserId);
            return res;
        }
    }
}
