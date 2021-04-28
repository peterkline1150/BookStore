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
    public class AuthorController : ApiController
    {
        private readonly AuthorService _service = new AuthorService();

        [Authorize]
        public IHttpActionResult Post(AuthorCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.CreateAuthor(model))
            {
                return InternalServerError();
            }

            return Ok("Author was added successfully!");
        }

        public IHttpActionResult Get()
        {
            var author = _service.GetAllAuthors();

            var authorsAlphabetizedByName = author.OrderBy(x => x.AuthorName);

            return Ok(authorsAlphabetizedByName);
        }

        [HttpGet]
        public IHttpActionResult Get(int authorId)
        {
            var author = _service.GetAuthorByAuthorId(authorId);

            return Ok(author);
        }

        [HttpGet]
        public IHttpActionResult Get(string authorName)
        {
            var author = _service.GetAuthorByAuthorName(authorName);

            return Ok(author);
        }

        [Authorize]
        public IHttpActionResult Put(AuthorUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_service.UpdateAuthor(model))
            {
                return InternalServerError();
            }

            return Ok("Author was updated successfully!");
        }

        [Authorize]
        public IHttpActionResult Delete(int authorId)
        {
            if (!_service.DeleteAuthor(authorId))
            {
                return InternalServerError();
            }

            return Ok("Author removed.");
        }
    }
}
