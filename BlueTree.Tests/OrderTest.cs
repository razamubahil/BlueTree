using BlueTree.SuperMarketEntities.Order;
using BlueTree.SuperMarketEntities.Product;

namespace BlueTree.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase("ABCDEABCDEABCDE")]
        public void Test_ParseLineItems(string LineItems)
        {
            ///Arrange Test
            IOrderCart cart = new OrderCart();

            List<ILineItem> lineItems = cart.ParseAndAddLineItems(LineItems);

            Assert.That(LineItems.Distinct().Count, Is.EqualTo(lineItems.Count));

            Assert.That(LineItems.Length, Is.EqualTo(lineItems.Sum(li => li.Qty)));
        }


        [Test]
        [TestCase("A", 50, 3, 130)]
        [TestCase("B", 30, 2, 45)]
        public void Test_CreateValidSku_WithDiscount(string SkuName, decimal NormalPrice, int DiscountQty, decimal DiscountPrice)
        {
            // Arrange Test
            ISku sku = new Sku(SkuName, NormalPrice);
            sku.SetDiscount(DiscountQty, DiscountPrice);

            // Act Test
            Assert.That(sku.SkuName, Is.EqualTo(SkuName));
            Assert.That(sku.NormalPrice, Is.EqualTo(NormalPrice));
            Assert.That(sku.Discount.Qty, Is.EqualTo(DiscountQty));
            Assert.That(sku.Discount.DiscountedPrice, Is.EqualTo(DiscountPrice));
            Assert.True(sku.HasDiscount());
        }

        [Test]
        [TestCase("A", 50)]
        [TestCase("B", 30)]
        public void Test_CreateValidSku_WithoutDiscount(string SkuName, decimal NormalPrice)
        {
            // Arrange Test
            ISku sku = new Sku(SkuName, NormalPrice);

            // Act Test
            Assert.That(sku.SkuName, Is.EqualTo(SkuName));
            Assert.That(sku.NormalPrice, Is.EqualTo(NormalPrice));
            Assert.False(sku.HasDiscount());
        }

        [Test]
        [TestCase("AAA", "A", 50, 3, 100)]
        [TestCase("BB", "B", 30, 2, 45)]
        public void Test_WhenOrderQtyEqualsDiscountedQty_ThenDiscountedPriceCalculation(string Items, string SkuId, decimal NormalPrice, int DiscountedQty, decimal DiscountedPrice)
        {
            ///Arrange Test
            IOrderCart cart = new OrderCart();

            // Add SKU
            ISku skuA = new Sku(SkuId, NormalPrice);
            skuA.SetDiscount(DiscountedQty, DiscountedPrice);

            cart.Skus.Add(skuA);

            // Add Items
            cart.ParseAndAddLineItems(Items);

            ///Act Test
            var price = cart.CalculatePrice();

            ///Asset Test
            Assert.That(price, Is.EqualTo(DiscountedPrice));
        }

        [Test]
        [TestCase("AAAA", "A", 50, 5, 100)]
        [TestCase("BBB", "B", 30, 4, 45)]
        public void Test_WhenOrderQtyLessThanDiscountedQty_ThenNormalPriceCalculation(string Items, string SkuId, decimal NormalPrice, int DiscountedQty, decimal DiscountedPrice)
        {
            ///Arrange Test
            IOrderCart cart = new OrderCart();

            // Add SKU
            ISku skuA = new Sku(SkuId, NormalPrice);
            skuA.SetDiscount(DiscountedQty, DiscountedPrice);

            cart.Skus.Add(skuA);

            ///Act Test
            cart.ParseAndAddLineItems(Items);
            var price = cart.CalculatePrice();

            ///Asset Test
            Assert.That(price, Is.EqualTo(NormalPrice * Items.Length));
        }

        [Test]
        [TestCase("AAAA", "A", 50, 3, 130, 180)]
        [TestCase("BBBBB", "B", 30, 2, 45, 120)]
        public void Test_WhenOrderQtyNotMultipleOfDiscountQty_ThenMixCalculation(string Items, string SkuId, decimal NormalPrice, int DiscountedQty, decimal DiscountedPrice, decimal ExpectedPrice)
        {
            ///Arrange Test
            IOrderCart cart = new OrderCart();

            // Add SKU
            ISku skuA = new Sku(SkuId, NormalPrice);
            skuA.SetDiscount(DiscountedQty, DiscountedPrice);

            cart.Skus.Add(skuA);

            // Add Items
            cart.ParseAndAddLineItems(Items);

            ///Act Test
            var price = cart.CalculatePrice();

            ///Asset Test
            Assert.That(price, Is.EqualTo(ExpectedPrice));
        }

        [Test]
        [TestCase("E")]
        [TestCase("ABECD")]
        public void Test_WhenIncorrectProductAdded_ThenException_SKUDoesNotExist(string Items)
        {
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
            ILineItem product;
            Items.ToCharArray().ToList().ForEach(
                (x =>
                {
                    product = new LineItem(x.ToString());
                    cart.CartItems.Add(product);
                }));


            ///Asset Test
            var ex = Assert.Throws<ArgumentException>(() => cart.CalculatePrice());

            Assert.That(ex.Message, Is.EqualTo("Attempt to add invalid product SKU. Invalid SKU (E)."));
        }

        [Test]
        [TestCase("AAA", 130)]
        [TestCase("BB", 45)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        [TestCase("ABC", 100)]
        [TestCase("AAABBCD", 210)]
        [TestCase("DCBACBABAA", 310)]
        public void Test_EndToEnd_HappyPath(string Items, decimal ExpectedPrice)
        {
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

            ///Act Test
            var price = cart.CalculatePrice();

            ///Asset Test
            Assert.That(price, Is.EqualTo(ExpectedPrice));
        }
    }
}