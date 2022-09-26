using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Models.Repositories
{
    public class BookRepository : IBookRepository<Book>
    {
        IList<Book> books;
        public BookRepository()
        {
            books = new List<Book>() {

            new Book{
                Id = 1,
                Title="Python book",
                Description="Python book",
                Author=new Author{ Id=2,},
                ImageUrl="python.png",
                },
           new Book{
                Id=2,
                Title="C++ book",
                Description="C++ book",
                Author=new Author(),
                ImageUrl="c++.png",

                },
           new Book{
                Id=3,
                Title="C# book",
                Description="C# book",
                Author=new Author(),
                },
            };
        }
        public void Add(Book item)
        {

            item.Id = books.Max(b => b.Id) + 1;
            books.Add(item);
        }

        public void Delete(int id)
        {
            books.Remove(Find(id));
        }

        public Book Find(int id)
        {
            return books.SingleOrDefault(x => x.Id == id);

        }

        public IList<Book> List()
        {
            return books;
        }

        public IList<Book> Search(string term)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Book item)
        {
            var book = Find(id);
            book.Title = item.Title;
            book.Description = item.Description;
            book.Author = item.Author;
            book.ImageUrl = item.ImageUrl;

        }
    }
}
