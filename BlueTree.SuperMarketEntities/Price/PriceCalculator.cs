using BlueTree.SuperMarketEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Price
{
    public class PriceCalculator : IPriceCalculator
    {
        public virtual decimal CalculatePrice(int Qty, ISku sku)
        {
            ValidateSku(sku);

            IPriceCalculator priceCalculator;
            decimal totalPrice = 0;

            if (sku.HasDiscount())
            {
                priceCalculator = new DiscountPriceCalculator();
                totalPrice = priceCalculator.CalculatePrice(Qty, sku);
            }
            else
            {
                priceCalculator = new SimplePriceCalculator();
                totalPrice = priceCalculator.CalculatePrice(Qty, sku);
            }
            return totalPrice;
        }

        private void ValidateSku(ISku Sku)
        {
            if (Sku == null)
            {
                throw new ArgumentException($"Pricing does not exist for the Product.");
            }
        }
    }
}
