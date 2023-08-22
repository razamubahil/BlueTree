using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Product
{
    public interface ISku
    {
        string SkuName { get; }

        decimal NormalPrice { get; set; }

        IDiscount Discount { get; }

        public void SetDiscount(int Qty, decimal DiscountPrice);

        public bool HasDiscount() => Discount != null;
    }
}
