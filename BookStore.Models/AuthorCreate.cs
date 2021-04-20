using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class AuthorCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
    }
}