using BookStore.Models;
using BookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.WebAPI.Controllers
{
    [Authorize]
    public class BookController : ApiController
    {
        private readonly BookService _service = new BookService();

        public IHttpActionResult Post(BookCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.CreateBook(model))
            {
                return InternalServerError();
            }

            return Ok("Book was created successfully!");
        }

        public IHttpActionResult Get()
        {
            var books = _service.GetBooks();

            var booksAlphabetizedByTitle = books.OrderBy(x => x.Title);

            return Ok(booksAlphabetizedByTitle);
        }

        public IHttpActionResult Get(DateTime startDate, DateTime endDate)
        {
            var books = _service.GetBooksByDate(startDate, endDate);
            var booksOrdered = books.OrderBy(x => x.Date);

            return Ok(booksOrdered);
        }

        public IHttpActionResult Put(int bookId, BookUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.UpdateBook(bookId, model))
            {
                return InternalServerError();
            }

            return Ok("Book was updated successfully!");
        }

        public IHttpActionResult Delete(int bookId)
        {
            if (!_service.DeleteBook(bookId))
            {
                return InternalServerError();
            }

            return Ok("Book was deleted successfully!");
        }
    }
}
