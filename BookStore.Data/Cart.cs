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
        public List<Book> BookList { get; set; } = new List<Book>();
        public double TotalCost { get
            {
                double totalCost = 0;
                foreach (var book in BookList)
                {
                    totalCost += (book.NumCopiesInCart * book.Price);
                }
                return Math.Round(totalCost * Tax, 2);
            } }
        public Guid BuyerId { get; set; }

    }
}
