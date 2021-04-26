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
        public bool CreateBook(BookCreate model)
        {
            var entity = new Book()
            {
                Title = model.Title,
                AuthorId = model.AuthorId,
                GenreId = model.GenreId,
                CompanyId = model.CompanyId,
                Date = model.Date,
                NumCopies = model.NumCopies,
                Price = model.Price,
                CartId = null,
                Cart = null
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

        public IEnumerable<BookList> GetBooks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Books.Include(e => e.Author);
                
                var listOfBooks = new List<BookList>();
                foreach (var book in query)
                {
                    var dateAsString = book.Date.ToShortDateString();
                    listOfBooks.Add(new BookList()
                    {
                        Title = book.Title,
                        AuthorName = book.Author.AuthorName,
                        Date = dateAsString,
                        BookId = book.BookId
                    });
                }

                return listOfBooks.ToArray();
            }
        }

        public IEnumerable<BookList> GetBooksByDate(DateTime startDate, DateTime endDate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Books.Include(e => e.Author).Where(e => e.Date >= startDate && e.Date <= endDate);

                var listOfBooks = new List<BookList>();
                foreach (var book in query)
                {
                    var dateAsString = book.Date.ToShortDateString();
                    listOfBooks.Add(new BookList()
                    {
                        Title = book.Title,
                        AuthorName = book.Author.AuthorName,
                        Date = dateAsString,
                        BookId = book.BookId
                    });
                }

                return listOfBooks.ToArray();
            }
        }

        public BookDetail GetBookById(int bookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Include(e => e.Author).Include(e => e.PublishingCompany).Include(e => e.Genre).Include(e => e.RatingsForBook)
                    .Single(e => e.BookId == bookId);

                var listOfRatings = new List<RatingForListInBookDetail>();
                foreach (var rating in entity.RatingsForBook)
                {
                    listOfRatings.Add(new RatingForListInBookDetail()
                    {
                        ScoreAverage = rating.ScoreAverage,
                        Description = rating.Description,
                        UserId = rating.UserId
                    });
                }
                return new BookDetail()
                {
                    BookId = entity.BookId,
                    Title = entity.Title,
                    AuthorName = entity.Author.AuthorName,
                    GenreName = entity.Genre.GenreName,
                    PublishingCompanyName = entity.PublishingCompany.PublishingCompanyName,
                    Date = entity.Date.ToShortDateString(),
                    NumCopies = entity.NumCopies,
                    IsAvailable = entity.IsAvailable,
                    Price = entity.Price.ToString("$0.00"),
                    AvRating = entity.AvRating,
                    IsRecommended = entity.IsRecommended,
                    RatingsForBook = listOfRatings
                };
            }
        }

        public BookDetail GetBookByName(string bookName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Include(e => e.Author).Include(e => e.PublishingCompany).Include(e => e.Genre).Include(e => e.RatingsForBook)
                    .Single(e => e.Title == bookName);

                var listOfRatings = new List<RatingForListInBookDetail>();
                foreach (var rating in entity.RatingsForBook)
                {
                    listOfRatings.Add(new RatingForListInBookDetail()
                    {
                        ScoreAverage = rating.ScoreAverage,
                        Description = rating.Description,
                        UserId = rating.UserId
                    });
                }
                return new BookDetail()
                {
                    BookId = entity.BookId,
                    Title = entity.Title,
                    AuthorName = entity.Author.AuthorName,
                    GenreName = entity.Genre.GenreName,
                    PublishingCompanyName = entity.PublishingCompany.PublishingCompanyName,
                    Date = entity.Date.ToShortDateString(),
                    NumCopies = entity.NumCopies,
                    IsAvailable = entity.IsAvailable,
                    Price = entity.Price.ToString("$0.00"),
                    AvRating = entity.AvRating,
                    IsRecommended = entity.IsRecommended,
                    RatingsForBook = listOfRatings
                };
            }
        }

        public bool UpdateBook(BookUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Find(model.BookId);

                int authorId = entity.AuthorId;
                int genreId = entity.GenreId;
                int companyId = entity.CompanyId;

                entity.Title = model.Title;
                entity.Date = model.Date;
                entity.NumCopies = model.NumCopies;
                entity.Price = model.Price;
                entity.AuthorId = model.AuthorId;
                entity.GenreId = model.GenreId;
                entity.CompanyId = model.CompanyId;

                if (authorId != entity.AuthorId)
                {
                    var authorEntity = ctx.Authors.Find(authorId);
                    authorEntity.BooksByAuthor.Remove(entity);

                    var newAuthorEntity = ctx.Authors.Find(entity.AuthorId);
                    newAuthorEntity.BooksByAuthor.Add(entity);
                }
                if (genreId != entity.GenreId)
                {
                    var genreEntity = ctx.Genres.Find(genreId);
                    genreEntity.BooksInGenre.Remove(entity);

                    var newGenreEntity = ctx.Genres.Find(entity.GenreId);
                    newGenreEntity.BooksInGenre.Add(entity);
                }
                if (companyId != entity.CompanyId)
                {
                    var companyEntity = ctx.PublishingCompanies.Find(companyId);
                    companyEntity.BooksPublished.Remove(entity);

                    var newCompanyEntity = ctx.PublishingCompanies.Find(entity.CompanyId);
                    newCompanyEntity.BooksPublished.Add(entity);
                }

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBook(int bookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Single(e => e.BookId == bookId);

                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
