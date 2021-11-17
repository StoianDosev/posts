using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IRepository<T> where T : class

    {

        Task<T> Create(T item);
        Task Delete(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}