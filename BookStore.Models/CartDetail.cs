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
        public double Tax { get; set; }
        public double TotalCost { get; set; }
        public List<string> TitlesOfBooksInCart { get; set; } = new List<string>();
    }
}
