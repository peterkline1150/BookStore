using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class RatingUpdate
    {
        public int RatingId { get; set; }

        public double EnjoymentScore { get; set; }

        public double EngagementScore { get; set; }

        public double AuthorStyleScore { get; set; }

        [MaxLength(200, ErrorMessage = "Be less verbose, please!")]
        public string Description { get; set; }
    }
}
