using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Models.Repositories
{
    public class AuthorDbRepository : IBookRepository<Author>
    {

        BookStoreDbContext db;
        public AuthorDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Author item)
        {
            db.Authors.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Authors.Remove(Find(id));
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            return db.Authors.SingleOrDefault(x => x.Id == id);
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public IList<Author> Search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author item)
        {
            db.Update(item);
            db.SaveChanges();

        }
    }
}
