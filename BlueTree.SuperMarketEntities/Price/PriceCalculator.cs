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
            if (Qty <= 0)
                throw new ArgumentException("Quantity is invalid");
            
            ValidateSku(sku);

            IPriceCalculator priceCalculator;
            decimal totalPrice = 0;
            try
            {
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
            }
            catch (DivideByZeroException)
            {
                throw new ArgumentException("Please check the lineItems and SKU discount setup.");
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
