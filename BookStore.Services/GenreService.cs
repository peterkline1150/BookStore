using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
                    GenreId = e.GenreId,
                    GenreName = e.GenreName
                });
                return query.ToArray();
            }
        }

        public GenreDetail GetAllBooksInGenre(int genreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Include(e => e.BooksInGenre)
                    .Single(e => e.GenreId == genreId);

                var titlesOfBooksInGenre = new List<string>();
                foreach (var book in entity.BooksInGenre)
                {
                    titlesOfBooksInGenre.Add(book.Title);
                }

                return new GenreDetail()
                {
                    GenreId = entity.GenreId,
                    GenreName = entity.GenreName,
                    BooksTitlesInGenre = titlesOfBooksInGenre
                }; 
            }
        }

        public GenreDetail GetGenreByGenreName(string genreName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Include(e => e.BooksInGenre).Single(e => genreName == e.GenreName);

                var titlesOfBooksInGenre = new List<string>();

                foreach (var book in entity.BooksInGenre)
                {
                    titlesOfBooksInGenre.Add(book.Title);
                }

                return new GenreDetail()
                {
                    GenreId = entity.GenreId,
                    GenreName = entity.GenreName,
                    BooksTitlesInGenre = titlesOfBooksInGenre
                };
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
