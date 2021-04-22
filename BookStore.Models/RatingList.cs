using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class RatingList
    {
        public int RatingId { get; set; }

        public double EnjoymentScore { get; set; }

        public double EngagementScore { get; set; }

        public double AuthorStyleScore { get; set; }

        public double ScoreAverage
        {
            get
            {
                var totalScore = EnjoymentScore + EngagementScore + AuthorStyleScore;
                return totalScore / 3;
            }
        }
    }
}