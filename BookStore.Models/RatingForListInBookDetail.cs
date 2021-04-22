using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class RatingForListInBookDetail
    {
        public Guid UserId { get; set; }

        public double ScoreAverage { get;  set; }

        public string Description { get; set; }
    }
}
