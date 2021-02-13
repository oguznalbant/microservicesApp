﻿using System.Collections.Generic;

namespace ECom.Cart.Api.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
        }

        public ShoppingCart(string username)
        {
            Username = username;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in ShoppingCartItems)
                {
                    totalPrice += (item.Price * item.Quantity);
                }

                return totalPrice;
            }
        }
    }
}
