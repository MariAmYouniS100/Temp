using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_layer.interfaces
{
    public interface IGenericRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync();   

        Task<T> GetByIdAsync(int id);  

        Task AddAsync(T entity);  

        void UpdateAsync(T entity);

        void DeleteAsync(T entity);
    }

}
// testing push 
// testing push 2