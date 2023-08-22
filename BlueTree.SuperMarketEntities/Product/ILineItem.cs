namespace BlueTree.SuperMarketEntities.Product
{
    public interface ILineItem
    {
        string ProductName { get; set; }

        int Qty { get; set; }
    }
}