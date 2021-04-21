using System;
using System.Collections.Generic;
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

        public string Description { get; set; }
    }
}
