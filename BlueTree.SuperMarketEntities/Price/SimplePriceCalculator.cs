﻿using BlueTree.SuperMarketEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Price
{
    public class SimplePriceCalculator : IPriceCalculator
    {
        /// <summary>
        /// Calcualte the simple price of the items
        /// </summary>
        /// <param name="Qty">Number of items to purchase</param>
        /// <param name="Sku">Type of item to purchase</param>
        /// <returns></returns>
        public virtual decimal CalculatePrice(int Qty, ISku Sku)
        {
            this.ValidateSku(Sku);
            return Qty * Sku.NormalPrice;
        }

        /// <summary>
        /// Validate that any invalid SKU are not added to the cart
        /// </summary>
        /// <param name="Sku">Valid Sku</param>
        /// <exception cref="ArgumentException"></exception>
        public void ValidateSku(ISku Sku)
        {
            if (Sku == null)
            {
                throw new ArgumentException($"Pricing does not exist for the Product.");
            }
        }
    }
}
