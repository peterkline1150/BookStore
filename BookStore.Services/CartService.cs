using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BookStore.Services
{
    public class CartService
    {
        private readonly Guid _buyerId;
        public CartService(Guid buyerId)
        {
            _buyerId = buyerId;
        }

        public bool AddBookToCart(int bookIdToAdd, int numberOfCopiesToAdd)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var doesExist = false;
                foreach (var cart in ctx.Cart)
                {
                    if (cart.BuyerId == _buyerId)
                    {
                        doesExist = true;
                    }
                }

                if (!doesExist)
                {
                    var entity = new Cart()
                    {
                        BuyerId = _buyerId
                    };

                    ctx.Cart.Add(entity);
                    ctx.SaveChanges();
                }

                var cartUpdateEntity = ctx.Cart.Single(e => e.BuyerId == _buyerId);

                var bookEntity = ctx.Books.Single(e => e.BookId == bookIdToAdd);

                if (!bookEntity.IsAvailable || bookEntity.NumCopies < numberOfCopiesToAdd)
                {
                    return false;
                }

                int bookIdToAddTo = 0;
                foreach (var book in cartUpdateEntity.BookList)
                {
                    if (book.BookId == bookIdToAdd)
                    {
                        bookIdToAddTo = book.BookId;
                    }
                }

                if (bookIdToAddTo == 0)
                {
                    var bookInCart = new BooksInIndividualCarts()
                    {
                        BookId = bookEntity.BookId,
                        Title = bookEntity.Title,
                        NumberOfThisBookInCart = numberOfCopiesToAdd,
                        UserId = _buyerId
                    };
                    cartUpdateEntity.BookList.Add(bookInCart);
                    ctx.BooksInCart.Add(bookInCart);

                    bookInCart.CartId = cartUpdateEntity.CartId;

                    bookEntity.NumCopies -= numberOfCopiesToAdd;

                    cartUpdateEntity.Cost += bookEntity.Price * bookInCart.NumberOfThisBookInCart;

                    return ctx.SaveChanges() >= 1;
                }
                else
                {
                    var book = ctx.Books.Single(e => e.BookId == bookIdToAdd);
                    var bookInCart = ctx.BooksInCart.Single(e => e.BookId == book.BookId && e.UserId == _buyerId);

                    book.NumCopies -= numberOfCopiesToAdd;
                    bookInCart.NumberOfThisBookInCart += numberOfCopiesToAdd;

                    cartUpdateEntity.Cost += book.Price * numberOfCopiesToAdd;

                    return ctx.SaveChanges() >= 1;
                }
            }
        }

        public bool RemoveBookFromCart(int bookIdToRemove, int numberOfCopiesToRemove)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cartUpdateEntity = ctx.Cart.Single(e => e.BuyerId == _buyerId);

                var bookEntity = ctx.Books.Single(e => e.BookId == bookIdToRemove);
                var bookInCartEntity = ctx.BooksInCart.Single(e => e.BookId == bookEntity.BookId && e.UserId == _buyerId);

                if (bookInCartEntity.NumberOfThisBookInCart > numberOfCopiesToRemove)
                {
                    bookInCartEntity.NumberOfThisBookInCart -= numberOfCopiesToRemove;
                    bookEntity.NumCopies += numberOfCopiesToRemove;

                    cartUpdateEntity.Cost -= bookEntity.Price * numberOfCopiesToRemove;

                    return ctx.SaveChanges() >= 1;
                }
                else if (bookInCartEntity.NumberOfThisBookInCart == numberOfCopiesToRemove)
                {
                    bookEntity.NumCopies += numberOfCopiesToRemove;
                    ctx.BooksInCart.Remove(bookInCartEntity);
                    cartUpdateEntity.BookList.Remove(bookInCartEntity);

                    cartUpdateEntity.Cost -= bookEntity.Price * numberOfCopiesToRemove;

                    return ctx.SaveChanges() >= 1;
                }

                return false;
            }
        }

        //View Cart
        public CartDetail ViewCart()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cartEntity = ctx.Cart.Include(e => e.BookList)
                    .Single(e => e.BuyerId == _buyerId);

                var listOfBooks = new List<BookItemInCart>();
                foreach (var bookInCart in cartEntity.BookList)
                {
                    listOfBooks.Add(new BookItemInCart()
                    {
                        Title = bookInCart.Title,
                        NumCopiesInCart = bookInCart.NumberOfThisBookInCart
                    });
                }

                return new CartDetail()
                {
                    TitlesAndNumCopiesOfBooksInCart = listOfBooks,
                    Tax = "7%",
                    TotalCost = cartEntity.TotalCost.ToString("$0.00")
                };
            }
        }

        //Purchase
        public double PurchaseBooksInCart()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cartEntity = ctx.Cart.Include(e => e.BookList).Single(e => e.BuyerId == _buyerId);
                double totalCost = cartEntity.TotalCost;

                var booksToRemove = new List<BooksInIndividualCarts>();
                foreach (var bookInCart in cartEntity.BookList)
                {
                    var bookInCartToRemove = ctx.BooksInCart.Single(e => e.BookId == bookInCart.BookId && e.UserId == cartEntity.BuyerId);
                    booksToRemove.Add(bookInCartToRemove);
                }

                foreach (var bookInCart in booksToRemove)
                {
                    ctx.BooksInCart.Remove(bookInCart);
                }

                ctx.Cart.Remove(cartEntity);
                ctx.SaveChanges();
                return totalCost;
            }
        }

    }
}
