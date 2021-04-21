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
    public class PublishingCompanyService
    {
        public bool CreatePublishingCompany(PublishingCompanyCreate model)
        {
            var entity = new PublishingCompany()
            {
                PublishingCompanyName = model.PublishingCompanyName,
                PublishingCompanyAddress = model.PublishingCompanyAddress
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.PublishingCompanies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PublishingCompanyList> GetAllPublishingCompanies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.PublishingCompanies.Select(e =>
                new PublishingCompanyList()
                {
                    PublishingCompanyName = e.PublishingCompanyName,
                    PublishingCompanyId = e.PublishingCompanyId
                });

                return query.ToArray();
            }
        }

        public PublishingCompanyDetail GetPublishingCompanyById(int companyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PublishingCompanies.Include(e => e.BooksPublished).Single(e => e.PublishingCompanyId == companyId);

                return new PublishingCompanyDetail()
                {
                    PublishingCompanyId = entity.PublishingCompanyId,
                    PublishingCompanyName = entity.PublishingCompanyName,
                    PublishingCompanyAddress = entity.PublishingCompanyAddress
                };
            }
        }

        public bool UpdatePublishingCompany(int companyId, PublishingCompanyUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PublishingCompanies.Single(e => e.PublishingCompanyId == companyId);

                entity.PublishingCompanyName = model.PublishingCompanyName;
                entity.PublishingCompanyAddress = model.PublishingCompanyAddress;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePublishingCompany(int companyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PublishingCompanies.Single(e => e.PublishingCompanyId == companyId);
                ctx.PublishingCompanies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
