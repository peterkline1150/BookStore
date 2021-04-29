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
    public class GenreController : ApiController
    {
        private readonly GenreService _service = new GenreService();


        [Authorize]
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

        [HttpGet]
        public IHttpActionResult Get(int genreId)
        {
            var genre = _service.GetAllBooksInGenre(genreId);
            if (genre != null)
            {
                return Ok(genre);
            }
            return BadRequest("That ID does not exist.");
        }

        [HttpGet]
        public IHttpActionResult Get(string genreName)
        {
            var genre = _service.GetGenreByGenreName(genreName);
            if (genre != null)
            {
                return Ok(genre);
            }
            return BadRequest("That Name does not exist.");
        }

        [Authorize]
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

        [Authorize]
        public IHttpActionResult Delete(int genreId)
        {
            if (!_service.DeleteGenre(genreId))
            {
                return BadRequest("Genre must be clear of all books before being deleted.");
            }
            return Ok("Genre removed.");
        }
    }
}
