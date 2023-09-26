using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //get All entities
        IEnumerable<T> GetAll();

        //Get by Id
        T GetById(int id);

        //Add
        Task<string> Add(T entity);

        //edit
        string Edite(int id, T entityy);

        //remove
        string Remove(int id);


    }
}
