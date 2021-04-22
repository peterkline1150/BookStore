using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class RatingCreate
    {
        [Required, Range(0, 5)]
        public double EnjoymentScore { get; set; }

        [Required, Range(0, 5)]
        public double EngagementScore { get; set; }

        [Required, Range(0, 5)]
        public double AuthorStyleScore { get; set; }

        [MaxLength(200, ErrorMessage = "Be less verbose, please!")]
        public string Description { get; set; }

        public int BookId { get; set; }
    }
}
