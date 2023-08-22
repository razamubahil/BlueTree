using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Product
{
    public class Sku : ISku
    {
        private string _skuName;
        public string SkuName
        {
            get { return _skuName; }
            set { _skuName = value; }
        }

        private decimal _productPrice;
        public decimal NormalPrice
        {
            get { return _productPrice; }
            set { _productPrice = value; }
        }

        private IDiscount _discount;
        public IDiscount Discount { get => _discount; }

        public Sku(string ProductTypeName, decimal Price)
        {
            _skuName = ProductTypeName;
            _productPrice = Price;
        }

        public void SetDiscount(int Qty, decimal DiscountPrice)
        {
            _discount = new Discount(Qty, DiscountPrice);
        }
    }
}
