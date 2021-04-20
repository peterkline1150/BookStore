using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService
    {
        public bool CreateBook (BookCreate model)
        {
            var entity = new Book()
            {
                Title = model.Title,
                AuthorId = model.AuthorId,
                GenreId = model.GenreId,
                CompanyId = model.CompanyId,
                Date = model.Date,
                NumCopies = model.NumCopies,
                Price = model.Price
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(entity);
                var authorEntity = ctx.Authors.Find(model.AuthorId);
                var genreEntity = ctx.Genres.Find(model.GenreId);
                var companyEntity = ctx.PublishingCompanies.Find(model.CompanyId);

                authorEntity.BooksByAuthor.Add(entity);
                genreEntity.BooksInGenre.Add(entity);
                companyEntity.BooksPublished.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookList> GetBooks ()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Books.Include(e => e.Author).Select(e =>
                new BookList()
                {
                    BookId = e.BookId,
                    Title = e.Title,
                    AuthorName = e.Author.AuthorName
                });

                return query.ToArray();
            }
        }
    }
}
