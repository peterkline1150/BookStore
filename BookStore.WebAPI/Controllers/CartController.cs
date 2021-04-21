﻿using BookStore.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.WebAPI.Controllers
{
    public class CartController : ApiController
    {
        private CartService CreateCartService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var cartService = new CartService(userId);
            return cartService;
        }

        public IHttpActionResult Put(int bookIdToAddOrRemove, int enterAnyNumberToRemove)
        {
            var _service = CreateCartService();

            if (!_service.UpdateCart(bookIdToAddOrRemove, enterAnyNumberToRemove))
            {
                return InternalServerError();
            }

            return Ok("Book was successfully added to your cart");
        }

        public IHttpActionResult Get()
        {
            var _service = CreateCartService();

            var cart = _service.ViewCart();

            return Ok(cart);
        }

        public IHttpActionResult Delete()
        {
            var _service = CreateCartService();

            var totalCostOfBooks = _service.PurchaseBooksInCart();

            return Ok($"You have purchased the book(s) in your cart. Your total comes to ${totalCostOfBooks:0.00}");
        }
    }
}