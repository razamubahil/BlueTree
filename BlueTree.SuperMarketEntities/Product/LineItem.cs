using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Product
{
    public class LineItem : ILineItem
    {
        public LineItem(string ProductName, int Qty = 1)
        {
            this.ProductName = ProductName;
            this.Qty = Qty;
        }

        public string ProductName { get; set; }
        public int Qty { get; set; }
    }
}
