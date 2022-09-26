using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Models.Repositories
{
    public class AuthorRepository : IBookRepository<Author>
    {

        IList<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>() {
            new Author{
                FullName="ahmed hassan",
            Id=1,
            },
            new Author{
                FullName="ali ibrahim",
            Id=2,
            },
            };
        }
        public void Add(Author item)
        {
            item.Id = authors.Max(b => b.Id) + 1;
            authors.Add(item);
        }

        public void Delete(int id)
        {
            authors.Remove(Find(id));
        }

        public Author Find(int id)
        {
            return authors.SingleOrDefault(x => x.Id == id);
        }

        public IList<Author> List()
        {
            return authors;
        }

        public IList<Author> Search(string term)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Author item)
        {
            var author = Find(id);
            author.FullName = item.FullName;

        }
    }
}
