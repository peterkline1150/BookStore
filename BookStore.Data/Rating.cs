using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required, Range(0,5)]
        public double EnjoymentScore { get; set; }

        [Required, Range(0,5)]
        public double EngagementScore { get; set; }

        [Required, Range(0,5)]
        public double AuthorStyleScore { get; set; }

        [Range(0,5)]
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
