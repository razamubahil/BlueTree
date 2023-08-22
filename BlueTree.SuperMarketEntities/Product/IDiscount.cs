using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Product
{
    public interface IDiscount
    {
        public int Qty { get; }
        public decimal DiscountedPrice { get; }
    }
}
