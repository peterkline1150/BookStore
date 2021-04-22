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
    public class AuthorController : ApiController
    {
        private readonly AuthorServices _service = new AuthorServices();

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

        public IHttpActionResult Get(int authorId)
        {
            var author = _service.GetAuthorByAuthorId(authorId);

            return Ok(author);
        }

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
