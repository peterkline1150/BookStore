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
    public class AuthorService
    {

        public bool CreateAuthor(AuthorCreate model)
        {
            var entity =
                new Author()
                {
                    AuthorName = model.AuthorName,
                    Birthdate = model.Birthdate
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Authors.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AuthorList> GetAllAuthors()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Authors.Select(e => new AuthorList
                {
                    AuthorId = e.AuthorId,
                    AuthorName = e.AuthorName
                });

                return query.ToArray();
            }
        }

        public AuthorDetail GetAuthorByAuthorId(int authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Authors.Include(e => e.BooksByAuthor).Single(e => authorId == e.AuthorId);

                var namesOfBooks = new List<string>();

                foreach (var book in entity.BooksByAuthor)
                {
                    namesOfBooks.Add(book.Title);
                }

                return new AuthorDetail()
                {
                    AuthorId = entity.AuthorId,
                    AuthorName = entity.AuthorName,
                    Birthdate = entity.Birthdate,
                    NamesOfBooksByAuthor = namesOfBooks
                };
            }
        }

        public bool UpdateAuthor(AuthorUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Authors.Single(e => e.AuthorId == model.AuthorId);

                entity.AuthorName = model.AuthorName;
                entity.Birthdate = model.Birthdate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAuthor(int authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Authors.Single(e => e.AuthorId == authorId);

                ctx.Authors.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}


