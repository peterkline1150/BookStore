using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CartUpdate
    {
        public double Tax { get; set; }
        public double TotalCost { get; set; }
        public string BookList { get; set; }
    }
}
