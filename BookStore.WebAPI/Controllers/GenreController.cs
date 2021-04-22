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
    public class GenreController : ApiController
    {
        private readonly GenreService _service = new GenreService();


        public IHttpActionResult Post(GenreCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_service.CreateGenre(model))
            {
                return InternalServerError();
            }
            return Ok("Genre created successfully!");
        }

        public IHttpActionResult Get()
        {
            var genres = _service.GetAllGenres();
            return Ok(genres);
        }

        public IHttpActionResult Get(int genreId)
        {
            var genre = _service.GetAllBooksInGenre(genreId);
            if (genre != null)
            {
                return Ok(genre);
            }
            return BadRequest("That ID does not exist.");
        }

        public IHttpActionResult Put(GenreUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_service.UpdateGenre(model))
            {
                return InternalServerError();
            }
            return Ok("Genre updated successfully! :^)");
        }

        public IHttpActionResult Delete(int genreId)
        {
            if (!_service.DeleteGenre(genreId))
            {
                return InternalServerError();
            }
            return Ok("Genre removed.");
        }




    }
}
