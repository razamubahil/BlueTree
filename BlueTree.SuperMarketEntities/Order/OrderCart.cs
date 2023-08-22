using BlueTree.SuperMarketEntities.Price;
using BlueTree.SuperMarketEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Order
{
    public class OrderCart : IOrderCart
    {
        public List<ISku> Skus { get; set; }

        public List<ILineItem> CartItems { get; set; }

        /// <summary>
        /// Initialise the class
        /// </summary>
        public OrderCart()
        {
            CartItems = new List<ILineItem>();
            Skus = new List<ISku>();
        }

        /// <summary>
        /// Parse the Line Items of the cart from the string Input
        /// </summary>
        /// <param name="LineItemsAsString">Pass the parameter as string with all Line Items e.g. ABCDABCABA</param>
        /// <returns>Return the list of line items added</returns>
        public List<ILineItem> ParseAndAddLineItems(string LineItemsAsString)
        {
            List<ILineItem> listOfCleanLineItems = new List<ILineItem>();
            ILineItem lineItem;

            var cartProducts = LineItemsAsString.GroupBy(n => n.ToString())
                          .Select(n => new
                          {
                              ProductName = n.Key,
                              Qty = n.Count()
                          })
                          .OrderBy(n => n.ProductName).ToList();


            cartProducts.ForEach(cartProduct =>
            {
                lineItem = new LineItem(cartProduct.ProductName, cartProduct.Qty);
                listOfCleanLineItems.Add(lineItem);
            });

            this.CartItems = listOfCleanLineItems;

            return this.CartItems;
        }



        /// <summary>
        /// Call the Pricing calculator based on the Discount Logic
        /// </summary>
        /// <returns>Total price of the cart</returns>
        /// <exception cref="ArgumentException"></exception>
        public decimal CalculatePrice()
        {
            IPriceCalculator priceCalculator;

            decimal totalPrice = 0;

            foreach (var lineItem in this.CartItems)
            {
                var skus = Skus.Where(sku => sku.SkuName == lineItem.ProductName);
                if (skus == null || !skus.Any())
                {
                    throw new ArgumentException($"Attempt to add invalid product SKU. Invalid SKU ({lineItem.ProductName}).");
                }

                if (skus.FirstOrDefault().HasDiscount())
                {
                    priceCalculator = new DiscountPriceCalculator();
                    totalPrice += priceCalculator.CalculatePrice(lineItem.Qty, skus.FirstOrDefault());
                }
                else
                {
                    priceCalculator = new SimplePriceCalculator();
                    totalPrice += priceCalculator.CalculatePrice(lineItem.Qty, skus.FirstOrDefault());
                }
            }

            return totalPrice;
        }
    }
}
