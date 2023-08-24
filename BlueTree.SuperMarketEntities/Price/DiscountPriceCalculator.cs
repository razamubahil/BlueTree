using BlueTree.SuperMarketEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Price
{
    public class DiscountPriceCalculator : SimplePriceCalculator
    {
        /// <summary>
        /// Calculate the discounted price
        /// </summary>
        /// <param name="Qty">Number of items to purchase</param>
        /// <param name="Sku">Type of item to purchase</param>
        /// <returns>Discounted Price</returns>
        public override decimal CalculatePrice(int Qty, ISku Sku)
        {
            decimal undiscountedPrice = 0;
            decimal discountedPrice = 0;
            if (Qty % Sku.Discount.Qty > 0)
            {
                undiscountedPrice = base.CalculatePrice(Qty % Sku.Discount.Qty, Sku);
            }
            discountedPrice = (Qty / Sku.Discount.Qty) * Sku.Discount.DiscountedPrice;
            return undiscountedPrice + discountedPrice;
        }
    }
}
