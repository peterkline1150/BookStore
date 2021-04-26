using BookStore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CartDetail
    {
        public string Tax { get; set; }
        public string TotalCost { get; set; }
        public List<BookItemInCart> TitlesAndNumCopiesOfBooksInCart { get; set; } = new List<BookItemInCart>();
    }
}
