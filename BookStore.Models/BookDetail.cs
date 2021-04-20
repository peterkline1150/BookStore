using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookDetail
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public Genre Genre { get; set; }

        public PublishingCompany PublishingCompany { get; set; }

        public DateTime Date { get; set; }

        public int NumCopies { get; set; }

        public bool IsAvailable
        {
            get
            {
                return NumCopies > 0;
            }
        }

        public double Price { get; set; }

        public List<Rating> RatingsForBook { get; set; } = new List<Rating>();

        public double AvRating
        {
            get
            {
                double totalRating = 0;
                foreach (Rating rating in RatingsForBook)
                {
                    totalRating += rating.ScoreAverage;
                }

                return RatingsForBook.Count > 0 ? Math.Round(totalRating / RatingsForBook.Count, 2) : 0;
            }
        }

        public bool IsRecommended
        {
            get
            {
                return AvRating >= 4;
            }
        }
    }
}
