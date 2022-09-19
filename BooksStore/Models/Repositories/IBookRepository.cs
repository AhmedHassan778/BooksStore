using System.Collections.Generic;

namespace BooksStore.Models.Repositories
{
    public interface IBookRepository<T>
    {
        IList<T> List();
        T Find(int id);
        void Add(T item);
        void Update(int id, T item);
        void Delete(int id);
    }
}
