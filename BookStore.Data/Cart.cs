﻿using System;
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
        [Required]
        public double Tax { get; set; }
        [Required]
        public double TotalCost { get; set; }
        public string BookList { get; set; }
        [Required]
        public Guid BuyerId { get; set; }

    }
}
