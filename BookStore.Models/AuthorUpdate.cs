using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class AuthorUpdate
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}