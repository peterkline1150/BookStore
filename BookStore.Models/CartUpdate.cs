using BookStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CartUpdate
    {
        public double TotalCost { get; set; }
        public List<Book> BookList { get; set; } = new List<Book>();

    }
}
