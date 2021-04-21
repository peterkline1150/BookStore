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

        public bool UpdateCart(int bookId, int enter0ToAddToCart1ToRemove)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (enter0ToAddToCart1ToRemove == 0)
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
                    var bookEntity = ctx.Books.Single(e => e.BookId == bookId);
                    cartUpdateEntity.BookList.Add(bookEntity);

                    return ctx.SaveChanges() == 1;
                }
                else
                {
                    var cartUpdateEntity = ctx.Cart.Single(e => e.BuyerId == _buyerId);
                    var bookEntity = ctx.Books.Single(e => e.BookId == bookId);
                    cartUpdateEntity.BookList.Remove(bookEntity);

                    return ctx.SaveChanges() == 1;
                }
            }
        }

        //View Cart
        public CartDetail ViewCart()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cartEntity = ctx.Cart.Include(e => e.BookList)
                    .Single(e => e.BuyerId == _buyerId);

                var listOfBookTitles = new List<string>();
                foreach (var book in cartEntity.BookList)
                {
                    listOfBookTitles.Add(book.Title);
                }

                return new CartDetail()
                {
                    TitlesOfBooksInCart = listOfBookTitles,
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
                var cartEntity = ctx.Cart.Single(e => e.BuyerId == _buyerId);
                double totalCost = cartEntity.TotalCost;

                ctx.Cart.Remove(cartEntity);
                ctx.SaveChanges();
                return totalCost;
            }
        }

    }
}
