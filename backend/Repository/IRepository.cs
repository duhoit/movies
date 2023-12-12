// Repositories/IRepository.cs
using System.Collections.Generic;

namespace backend.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
