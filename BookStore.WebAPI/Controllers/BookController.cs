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
    public class BookController : ApiController
    {
        private readonly BookService _service = new BookService();

        [Authorize]
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
            if (startDate > endDate)
            {
                return BadRequest("Start date must be earlier than end date.");
            }

            var books = _service.GetBooksByDate(startDate, endDate);
            var booksOrdered = books.OrderBy(x => x.Date);

            return Ok(booksOrdered);
        }

        [HttpGet]
        public IHttpActionResult Get(int bookId)
        {
            var book = _service.GetBookById(bookId);

            if (book != null)
            {
                return Ok(book);
            }

            return BadRequest("That ID does not exist.");
        }

        [HttpGet]
        public IHttpActionResult Get(string bookName)
        {
            var book = _service.GetBookByName(bookName);

            if (book != null)
            {
                return Ok(book);
            }

            return BadRequest("That Name does not exist.");
        }

        [Authorize]
        public IHttpActionResult Put(BookUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.UpdateBook(model))
            {
                return InternalServerError();
            }

            return Ok("Book was updated successfully!");
        }

        [Authorize]
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
