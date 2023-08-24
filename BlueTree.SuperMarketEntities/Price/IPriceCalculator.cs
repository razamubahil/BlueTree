using BlueTree.SuperMarketEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTree.SuperMarketEntities.Price
{
    interface IPriceCalculator
    {
        decimal CalculatePrice(int Qty, ISku sku);
    }
}
