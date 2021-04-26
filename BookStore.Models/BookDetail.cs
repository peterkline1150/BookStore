using BookStore.Data;
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

        public string AuthorName { get; set; }

        public string GenreName { get; set; }

        public string PublishingCompanyName { get; set; }

        public string Date { get; set; }

        public int NumCopies { get; set; }

        public bool IsAvailable { get; set; }

        public string Price { get; set; }

        public List<RatingForListInBookDetail> RatingsForBook { get; set; } = new List<RatingForListInBookDetail>();

        public double AvRating { get; set; }

        public bool IsRecommended { get; set; }
    }
}
