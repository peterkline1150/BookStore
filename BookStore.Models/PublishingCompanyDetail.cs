using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class PublishingCompanyDetail
    {
        public int PublishingCompanyId { get; set; }

        public string PublishingCompanyName { get; set; }

        public string PublishingCompanyAddress { get; set; }

        public List<string> BookTitlesByPublishingCompany { get; set; } = new List<string>();
    }
}
