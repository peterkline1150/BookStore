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
    public class PublishingCompanyController : ApiController
    {
        private readonly PublishingCompanyService _service = new PublishingCompanyService();

        [Authorize]
        public IHttpActionResult Post(PublishingCompanyCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.CreatePublishingCompany(model))
            {
                return InternalServerError();
            }

            return Ok("Publishing company created successfully!");
        }

        public IHttpActionResult Get()
        {
            var companies = _service.GetAllPublishingCompanies();

            var companiesOrderedByName = companies.OrderBy(x => x.PublishingCompanyName);

            return Ok(companiesOrderedByName);
        }

        [HttpGet]
        public IHttpActionResult Get(int companyId)
        {
            var company = _service.GetPublishingCompanyById(companyId);

            if (company != null)
            {
                return Ok(company);
            }

            return BadRequest("A publishing company does not exist with that ID");
        }

        [HttpGet]
        public IHttpActionResult Get(string companyName)
        {
            var company = _service.GetPublishingCompanyByName(companyName);

            if (company != null)
            {
                return Ok(company);
            }

            return BadRequest("A publishing company does not exist with that Name");
        }

        [Authorize]
        public IHttpActionResult Put(PublishingCompanyUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.UpdatePublishingCompany(model))
            {
                return InternalServerError();
            }

            return Ok("Publishing company updated successfully!");
        }

        [Authorize]
        public IHttpActionResult Delete(int companyId)
        {
            if (!_service.DeletePublishingCompany(companyId))
            {
                return InternalServerError();
            }

            return Ok("Publishing comany deleted successfully!");
        }
    }
}
