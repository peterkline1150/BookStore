using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class AuthorServices
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
    }
}

      
