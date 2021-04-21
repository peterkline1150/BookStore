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
        [Required]
        public double EnjoymentScore { get; set; }

        [Required]
        public double EngagementScore { get; set; }

        [Required]
        public double AuthorStyleScore { get; set; }
    }
}
