using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public double Tax { get 
            {
                return 1.07;
            } }
        public List<BooksInIndividualCarts> BookList { get; set; } = new List<BooksInIndividualCarts>();
        public double Cost { get; set; }
        public double TotalCost { get
            {
                return Math.Round(Cost * Tax, 2);
            } }
        public Guid BuyerId { get; set; }

    }
}
