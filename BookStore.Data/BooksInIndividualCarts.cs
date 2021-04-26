using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class BooksInIndividualCarts
    {
        [Key]
        public int Id { get; set; }

        public int NumberOfThisBookInCart { get; set; }

        public int BookId { get; set; }

        public Guid UserId { get; set; }
    }
}
