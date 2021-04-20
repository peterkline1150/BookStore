using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookUpdate
    {
        [MaxLength(100, ErrorMessage = "Title is too long")]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int CompanyId { get; set; }

        public DateTime Date { get; set; }

        public int NumCopies { get; set; }

        public double Price { get; set; }
    }
}
