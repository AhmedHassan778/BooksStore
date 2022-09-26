using BooksStore.Models;
using BooksStore.Models.Repositories;
using BooksStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BooksStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository<Book> bookRepository;
        private readonly IBookRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookRepository<Book> bookRepository,
            IBookRepository<Author> authorRepository,
            IHostingEnvironment hosting
            )
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
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
                Authors = FillSelectList(),
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {

            if (ModelState.IsValid)
                try

                {
                    string fileName = UploadFile(model.File, "");
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an author from the list";

                        return View(GetAllAuthors(model));
                    }
                    var book = new Book
                    {
                        Id = model.BookId,
                        Description = model.Description,
                        Author = authorRepository.Find(model.AuthorId),
                        Title = model.Title,
                        ImageUrl = fileName,
                    };
                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(model);
                }
            ModelState.AddModelError("", "You have to fill all the required fields!!");
            return View(GetAllAuthors(model));
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
                ImageUrl = book.ImageUrl,
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {

            try
            {
                string fileName = UploadFile(viewModel.File, viewModel.ImageUrl ?? "");
                var book = new Book
                {
                    Id = viewModel.BookId,
                    Description = viewModel.Description,
                    Author = authorRepository.Find(viewModel.AuthorId),
                    Title = viewModel.Title,
                    ImageUrl = fileName,
                };
                bookRepository.Update(viewModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(viewModel);
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);

            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {

            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "--- Please Select An Auhtor ---" });

            return authors;
        }

        BookAuthorViewModel GetAllAuthors(BookAuthorViewModel model)
        {
            model.Authors = FillSelectList();
            return model;
        }


        string UploadFile(IFormFile file, string oldImageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string newPath = Path.Combine(uploads, file.FileName);

                if (oldImageUrl.Length != 0)
                {
                    // delete the old file
                    string oldPath = Path.Combine(uploads, oldImageUrl);
                    System.IO.File.Delete(oldPath);
                }
                // save the new file   
                file.CopyTo(new FileStream(newPath, FileMode.Create));
                return file.FileName;
            }
            return oldImageUrl;
        }


        public ActionResult Search(string term)
        {

            var books = bookRepository.Search(term);
            return View("Index", books);
        }
    }


}
