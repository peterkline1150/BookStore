using BookStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class AuthorDetail
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Birthdate { get; set; }
        public List<string> NamesOfBooksByAuthor { get; set; } = new List<string>();
    }
}