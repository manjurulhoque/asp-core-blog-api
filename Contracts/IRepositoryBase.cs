using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogapi.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        ICollection<T> FindAll();
        T FindById(Guid id);
        bool isExists(Guid id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(Guid id);
        bool Save();
    }
}
