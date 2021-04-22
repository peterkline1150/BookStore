using BookStore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class RatingDetail
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

        public string Description { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
    }
}
