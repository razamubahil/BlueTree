using BlueTree.SuperMarketEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Order
{
    public interface IOrderCart
    {
        public List<ISku> Skus { get; set; }

        public List<ILineItem> CartItems { get; set; }

        public List<ILineItem> ParseAndAddLineItems(string LineItemsAsString);

        public decimal CalculatePrice();

    }
}
