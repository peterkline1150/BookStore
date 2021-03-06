using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public List<Book> BooksByAuthor { get; set; } = new List<Book>();
    }
}
