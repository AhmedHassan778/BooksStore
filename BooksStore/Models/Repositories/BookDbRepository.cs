using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Models.Repositories
{
    public class BookDbRepository : IBookRepository<Book>
    {
        BookStoreDbContext db;
        public BookDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book item)
        {
            db.Books.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Books.Remove(Find(id));
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            return db.Books.Include(b => b.Author).SingleOrDefault(x => x.Id == id);

        }

        public IList<Book> List()
        {
            return db.Books.Include(b => b.Author).ToList();
        }

        public void Update(int id, Book item)
        {
            db.Update(item);
            db.SaveChanges();
        }

        public IList<Book> Search(string term)
        {
            var result = db.Books.Include(b => b.Author)
                   .Where(b => b.Title.Contains(term)
                           || b.Description.Contains(term)
                           || b.Author.FullName.Contains(term)).ToList();

            return result;
        }


    }
}
