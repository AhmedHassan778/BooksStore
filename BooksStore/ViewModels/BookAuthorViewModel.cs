using BooksStore.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksStore.ViewModels
{
    public class BookAuthorViewModel
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [MaxLength(20), MinLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public List<Author> Authors { get; set; }

        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }

    }
}
