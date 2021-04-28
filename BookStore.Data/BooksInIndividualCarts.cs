using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    // This data is for individual instances of specific books in individual peoples' carts. This data will not have any models or a service associated with it,
    //    as this data is automatically created when the user adds a specific book to their cart, and it is automatically deleted when the user either removes
    //    a book completely from their cart or they checkout and purchase the books in their cart.
    public class BooksInIndividualCarts
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public int NumberOfThisBookInCart { get; set; }

        public int BookId { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
