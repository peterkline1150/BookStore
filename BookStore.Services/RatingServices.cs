using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class RatingServices
    {
        private readonly Guid _userId;

        public RatingServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRating(RatingCreate model)
        {
            var entity =
                new Rating()
                {
                    EnjoymentScore = model.EnjoymentScore,
                    EngagementScore = model.EngagementScore,
                    AuthorStyleScore = model.AuthorStyleScore,
                    Description = model.Description,
                    BookId = model.BookId,
                    UserId = _userId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Ratings.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateRating(RatingUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(e => e.RatingId == model.RatingId);

                entity.EnjoymentScore = model.EnjoymentScore;
                entity.EngagementScore = model.EngagementScore;
                entity.AuthorStyleScore = model.AuthorStyleScore;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRating(int ratingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(e => e.RatingId == ratingId);

                ctx.Ratings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
