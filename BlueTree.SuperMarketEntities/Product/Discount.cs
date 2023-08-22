using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Product
{
    public class Discount : IDiscount
    {
        private int _qty;
        public int Qty { get { return _qty; } }

        private decimal _discountedPrice;
        public decimal DiscountedPrice { get { return _discountedPrice; } }

        public Discount(int Qty, decimal DiscountedPrice)
        {
            if (Qty <= 0)
                throw new ArgumentException("Invalid Parameter value.", nameof(Qty));

            if (DiscountedPrice <= 0)
                throw new ArgumentException("Invalid Parameter value.", nameof(DiscountedPrice));

            _qty = Qty;
            _discountedPrice = DiscountedPrice;
        }
    }
}
