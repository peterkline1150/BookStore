using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNet.Identity;
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
        private RatingServices CreateRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var ratingService = new RatingServices(userId);
            return ratingService;
        }


        public IHttpActionResult Post(RatingCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _service = CreateRatingService();

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
                return BadRequest(ModelState);
            }

            var _service = CreateRatingService();

            if (!_service.UpdateRating(model))
            {
                return InternalServerError();
            }

            return Ok();
        }

        public IHttpActionResult Delete(int ratingId)
        {
            var _service = CreateRatingService();

            if (!_service.DeleteRating(ratingId))
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
