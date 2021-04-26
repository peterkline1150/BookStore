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

        //public bool UpdateCart(int bookId, int enter0ToAddToCart1ToRemove)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        if (enter0ToAddToCart1ToRemove == 0)
        //        {
        //            if (ctx.Cart.Count() == 0)
        //            {
        //                var entity = new Cart()
        //                {
        //                    BuyerId = _buyerId
        //                };

        //                ctx.Cart.Add(entity);
        //                ctx.SaveChanges();
        //            }

        //            var cartUpdateEntity = ctx.Cart.Single(e => e.BuyerId == _buyerId);
        //            var bookEntity = ctx.Books.Single(e => e.BookId == bookId);
        //            cartUpdateEntity.BookList.Add(bookEntity);

        //            bookEntity.CartId = cartUpdateEntity.CartId;

        //            return ctx.SaveChanges() == 1;
        //        }
        //        else
        //        {
        //            var cartUpdateEntity = ctx.Cart.Single(e => e.BuyerId == _buyerId);
        //            var bookEntity = ctx.Books.Single(e => e.BookId == bookId);
        //            cartUpdateEntity.BookList.Remove(bookEntity);

        //            return ctx.SaveChanges() == 1;
        //        }
        //    }
        //}

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
                    cartUpdateEntity.BookList.Add(bookEntity);
                    var bookInCart = new BooksInIndividualCarts()
                    {
                        BookId = bookEntity.BookId,
                        NumberOfThisBookInCart = numberOfCopiesToAdd,
                        UserId = _buyerId
                    };

                    ctx.BooksInCart.Add(bookInCart);

                    bookEntity.CartId = cartUpdateEntity.CartId;
                    bookEntity.NumCopies -= numberOfCopiesToAdd;

                    cartUpdateEntity.Cost += bookEntity.Price * bookInCart.NumberOfThisBookInCart;

                    return ctx.SaveChanges() >= 1;
                }
                else
                {
                    var book = cartUpdateEntity.BookList.Single(e => e.BookId == bookIdToAdd);
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
                    bookEntity.CartId = null;
                    bookEntity.NumCopies += numberOfCopiesToRemove;
                    ctx.BooksInCart.Remove(bookInCartEntity);
                    cartUpdateEntity.BookList.Remove(bookEntity);

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
                foreach (var book in cartEntity.BookList)
                {
                    var bookInCart = ctx.BooksInCart.Single(e => e.BookId == book.BookId && e.UserId == _buyerId);
                    listOfBooks.Add(new BookItemInCart()
                    {
                        Title = book.Title,
                        NumCopiesInCart = bookInCart.NumberOfThisBookInCart
                    });
                }

                return new CartDetail()
                {
                    TitlesAndNumCopiesOfBooksInCart = listOfBooks,
                    Tax = cartEntity.Tax,
                    TotalCost = cartEntity.TotalCost
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
                foreach (var book in cartEntity.BookList)
                {
                    var bookInCartToRemove = ctx.BooksInCart.Single(e => e.BookId == book.BookId && e.UserId == cartEntity.BuyerId);
                    booksToRemove.Add(bookInCartToRemove);

                    book.CartId = null;
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
