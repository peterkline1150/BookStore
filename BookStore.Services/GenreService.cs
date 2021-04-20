using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class GenreService
    {
        public bool CreateGenre(GenreCreate model)
        {
            var entity = new Genre()
            {
                GenreName = model.GenreName
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Genres.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GenreList> GetAllGenres()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Genres.Select(e => new GenreList
                {
                    GenreName = e.GenreName
                });
                return query.ToArray();
            }
        }
        public bool UpdateGenre(GenreUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Single(e => e.GenreId == model.GenreId);

                entity.GenreName = model.GenreName;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteGenre(int genreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Single(e => e.GenreId == genreId);

                ctx.Genres.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
