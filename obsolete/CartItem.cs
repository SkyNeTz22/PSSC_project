using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessing.Domain.CartModel
{
    public class CartItem
    {
        public List<string> ProductsList { get; private set; }
        public int CartPrice { get; private set; }

        public CartItem(List<string> productsList, int cartPrice)
        {
            ProductsList = productsList;
            CartPrice = cartPrice;
        }
    }
}
