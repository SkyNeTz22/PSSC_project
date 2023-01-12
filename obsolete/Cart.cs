using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessing.Domain.CartModel
{
    [AsChoice]
    public static partial class Cart
    {
        public interface ICart { }

        public class EmptyCart: ICart
        {

        }

        public class ActiveCart: ICart
        {
            public IReadOnlyCollection<CartItem> Items { get; private set; }

            internal ActiveCart(IReadOnlyCollection<CartItem> items)
            {
                Items = items;
            }
        }

        public class PaidCart: ICart
        {
            public DateTime PaymentDate { get; private set; }

            public IReadOnlyCollection<CartItem> Items { get; private set; }

            internal PaidCart(IReadOnlyCollection<CartItem> items, DateTime paymentDate)
            {
                Items = items;
                PaymentDate = paymentDate;
            }
        }
    }
}
