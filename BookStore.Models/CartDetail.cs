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
        public int CartId { get; set; }
        public double Tax { get; set; }
        public double TotalCost { get; set; }
        public string BookList { get; set; }
        public Guid BuyerId { get; set; }
    }
}
