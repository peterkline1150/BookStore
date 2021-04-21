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
    public class RatingController : ApiController
    {
        private readonly RatingServices _service = new RatingServices();

        public IHttpActionResult Post(RatingCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_service.CreateRating(model))
            {
                return InternalServerError();
            }

            return Ok();
        }

        public IHttpActionResult Put(RatingUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_service.UpdateRating(model))
            {
                return InternalServerError();
            }

            return Ok();
        }

        public IHttpActionResult Delete(int ratingId)
        {
            if (!_service.DeleteRating(ratingId))
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
