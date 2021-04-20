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

        [Required]
        public double EnjoymentScore { get; set; }

        [Required]
        public double EngagementScore { get; set; }

        [Required]
        public double AuthorStyleScore { get; set; }

        public double ScoreAverage { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
    }
}
