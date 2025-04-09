using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IGeneralRepository<T> where T : BaseModel
    {
        //Add
        Task<T> AddAsync(T model);

        //Get All
        IQueryable<T> GetAll();


        //Get By Id 
        Task<T> GetByIdAsync(int id);

        //Update  
        bool Update(int id, T model);


        //Delete
        Task<bool> Delete(int id);

        Task<bool> GetByIdAsyncAny(int id);
        Task<int> SaveChangesAsync();
 

    }
}
