using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class PublishingCompany
    {
        [Key]
        public int PublishingCompanyId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Company Name is too long")]
        public string PublishingCompanyName { get; set; }

        [Required]
        public string PublishingCompanyAddress { get; set; }
    }
}
