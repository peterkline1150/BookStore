using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CartPurchase
    {


        public double Tax { get; set; }
        public double TotalCost { get; set; }
        public string BookList { get; set; }

    }
}
