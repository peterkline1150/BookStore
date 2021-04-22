using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class PublishingCompanyUpdate
    {
        public int PublishingCompanyId { get; set; }

        [MaxLength(100, ErrorMessage = "Company Name is too long")]
        public string PublishingCompanyName { get; set; }

        public string PublishingCompanyAddress { get; set; }
    }
}
