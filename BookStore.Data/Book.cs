using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        public Cart Cart { get; set; }

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [ForeignKey(nameof(PublishingCompany))]
        public int CompanyId { get; set; }

        public PublishingCompany PublishingCompany { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int NumCopies { get; set; }

        public bool IsAvailable { get 
            {
                return NumCopies > 0;
            }
        }

        [Required]
        public double Price { get; set; }

        public List<Rating> RatingsForBook { get; set; } = new List<Rating>();

        public double AvRating { get 
            {
                double totalRating = 0;
                foreach (Rating rating in RatingsForBook)
                {
                    totalRating += rating.ScoreAverage;
                }

                return RatingsForBook.Count > 0 ? Math.Round(totalRating / RatingsForBook.Count, 2) : 0;
            }
        }

        public bool IsRecommended { get 
            {
                return AvRating >= 4;
            }
        }
    }
}
