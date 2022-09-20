using BooksStore.Models;
using BooksStore.Models.Repositories;
using BooksStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BooksStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository<Book> bookRepository;
        private readonly IBookRepository<Author> authorRepository;

        public BookController(IBookRepository<Book> bookRepository, IBookRepository<Author> authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {

            var model = new BookAuthorViewModel
            {
                Authors = authorRepository.List().ToList(),
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {


            try
            {
                var book = new Book
                {
                    Id = model.BookId,
                    Description = model.Description,
                    Author = authorRepository.Find(model.AuthorId),
                    Title = model.Title,
                };
                bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = bookRepository.Find(id);
            book.Author.Id = book.Author.Id == null ? 1 : book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Description = book.Description,
                Title = book.Title,
                AuthorId = book.Author.Id,
                Authors = authorRepository.List().ToList(),
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel viewModel)
        {

            try
            {
                var book = new Book
                {
                    Description = viewModel.Description,
                    Author = authorRepository.Find(viewModel.AuthorId),
                    Title = viewModel.Title,
                };
                bookRepository.Update(viewModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
