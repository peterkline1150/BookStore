using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookCreate
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        public int CompanyId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int NumCopies { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
