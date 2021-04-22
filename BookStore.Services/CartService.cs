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
                if (ctx.Cart.Count() == 0)
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

                if (bookIdToAddTo != 0)
                {
                    var book = cartUpdateEntity.BookList.Single(e => e.BookId == bookIdToAdd);
                    book.NumCopies -= numberOfCopiesToAdd;
                    book.NumCopiesInCart += numberOfCopiesToAdd;

                    return ctx.SaveChanges() == 1;
                }

                cartUpdateEntity.BookList.Add(bookEntity);

                bookEntity.CartId = cartUpdateEntity.CartId;
                bookEntity.NumCopiesInCart += numberOfCopiesToAdd;
                bookEntity.NumCopies -= numberOfCopiesToAdd;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool RemoveBookFromCart(int bookIdToRemove, int numberOfCopiesToRemove)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cartUpdateEntity = ctx.Cart.Single(e => e.BuyerId == _buyerId);

                var bookEntity = ctx.Books.Single(e => e.BookId == bookIdToRemove);

                if (bookEntity.NumCopiesInCart > numberOfCopiesToRemove)
                {
                    bookEntity.NumCopiesInCart -= numberOfCopiesToRemove;
                    bookEntity.NumCopies += numberOfCopiesToRemove;

                    return ctx.SaveChanges() == 1;
                }
                else if (bookEntity.NumCopiesInCart == numberOfCopiesToRemove)
                {
                    bookEntity.CartId = null;
                    bookEntity.NumCopies += numberOfCopiesToRemove;
                    bookEntity.NumCopiesInCart = 0;
                    cartUpdateEntity.BookList.Remove(bookEntity);

                    return ctx.SaveChanges() == 1;
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
                    listOfBooks.Add(new BookItemInCart()
                    {
                        Title = book.Title,
                        NumCopiesInCart = book.NumCopiesInCart
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

                foreach (var book in cartEntity.BookList)
                {
                    book.CartId = null;
                }

                ctx.Cart.Remove(cartEntity);
                ctx.SaveChanges();
                return totalCost;
            }
        }

    }
}
