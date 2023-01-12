using System;
using System.Collections.Generic;
using System.Text;
using static OrderProcessing.Domain.CartModel.Cart;

namespace OrderProcessing.Domain.CartModel
{
    public static class CartOperations
    {
        public static ICart AddItem(ICart cart, CartItem itemToAdd)
        {
            ICart newCart = cart.Match(
                    emptyCart =>
                    {
                        var items = new List<CartItem>() { itemToAdd };
                        var activeCart = new ActiveCart(items);
                        return activeCart;
                    },
                    activeCart =>
                    {
                        var items = new List<CartItem>();
                        items.AddRange(activeCart.Items);
                        items.Add(itemToAdd);
                        var newActiveCart = new ActiveCart(items);
                        return newActiveCart;
                    },
                    paidCart =>
                    {
                        //paid cart cannot be modified
                        //we could return an error
                        return paidCart;
                    }
                );

            return newCart;
        }
    }
}
