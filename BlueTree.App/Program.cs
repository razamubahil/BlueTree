using BlueTree.SuperMarketEntities.Order;
using BlueTree.SuperMarketEntities.Product;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter the line items as a string (e.g.) ABC : ");
        string Items = Console.ReadLine();

        ///Arrange Test
        IOrderCart cart = new OrderCart();

        // Add SKU
        ISku skuA = new Sku("A", 50);
        skuA.SetDiscount(3, 130);

        ISku skuB = new Sku("B", 30);
        skuB.SetDiscount(2, 45);

        ISku skuC = new Sku("C", 20);

        ISku skuD = new Sku("D", 15);

        cart.Skus.Add(skuA);
        cart.Skus.Add(skuB);
        cart.Skus.Add(skuC);
        cart.Skus.Add(skuD);


        // Add Items
        var allLineItems = cart.ParseAndAddLineItems(Items);

        try
        {
            ///Act Test
            var price = cart.CalculatePrice();


            Console.WriteLine($"Calculated price is : {price}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception occurred: {ex.Message}");
        }
        Console.ReadLine();
    }
}