using NUnit.Framework;
using System.Collections.Generic;

namespace DecisionTechPriceCalc.Tests
{
    public class BasketDiscountTests
    {

        List<Product> productList = new List<Product>(){new Product("Butter", 0.8m),
                                                       new Product("Milk", 1.15m),
                                                       new Product("Bread", 1)};

        private Product GetProduct(string productName)
        {
            Product product = productList.Find(x => x.Name == productName);
            if (product == null)
                throw new System.ArgumentException($"Unable to find product {productName}");
            return product;
        }

        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 1, 1, 1 }, 2.95, TestName = "No Offers - 1 Bread, 1 Butter, 1 Milk")]
        [TestCase(new string[] { "Bread", "Butter" }, new int[] { 2, 2 }, 3.6, TestName = "No Offers - 2 Bread, 2 Butter")]
        [TestCase(new string[] { "Milk" }, new int[] { 4 }, 4.6, TestName = "No Offers - 4 Milk")]
        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 1, 2, 8 }, 11.8, TestName = "No Offers - 1 Bread, 2 Butter, 8 Milk")]
        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 15, 17, 29 }, 61.95, TestName = "No Offers - 2 Offers - 15 Bread, 17 Butter, 29 Milk")]
        public void GivenABasketWithNoOffers(string[] product, int[] quantity, decimal expectedPrice)
        {
            if (product.Length != quantity.Length)
                throw new System.ArgumentException("Products and Quantity arrays must match");

            ProductPricer productCollectionPricer = new ProductPricer(new TestLogger());
            List<ProductInBasket> productsInBasket = new List<ProductInBasket>();

            // For each product, if this test requires it, put it into the basket with the appropriate quantity
            for (int i = 0; i < product.Length; i++)
                productsInBasket.Add(new ProductInBasket(GetProduct(product[i]), quantity[i]));

            var result = productCollectionPricer.GetPrice(productsInBasket, new IOffer[] { });

            Assert.That(result, Is.EqualTo(expectedPrice));
        }  
        
        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 1, 1, 1 }, 2.95, TestName = "2 Offers - 1 Bread, 1 Butter, 1 Milk")]
        [TestCase(new string[] { "Bread", "Butter" }, new int[] { 2, 2 }, 3.10, TestName = "2 Offers - 2 Bread, 2 Butter")]
        [TestCase(new string[] { "Milk" }, new int[] { 4 }, 3.45, TestName = "2 Offers - 4 Milk")]
        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 1, 2, 8 }, 9, TestName = "2 Offers - 1 Bread, 2 Butter, 8 Milk")]
        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 15, 17, 29 }, 49.9, TestName = "2 Offers - 15 Bread, 17 Butter, 29 Milk")]
        [TestCase(new string[] { "Bread", "Butter" }, new int[] { 6, 52 }, 44.6, TestName = "2 Offers - Large quantity  of Butter, small quantity Bread -  6 Bread, 52 Butter")]
        [TestCase(new string[] { "Bread", "Butter" }, new int[] { 23, 5 },26, TestName = "2 Offers - Large quantity of Bread, small quantity of butter-  23 Bread, 5 Butter")]
        public void GivenABasketWithTwoOffers(string[] product, int[] quantity, decimal expectedPrice)
        {
            if (product.Length != quantity.Length)
                throw new System.ArgumentException("Products and Quantity arrays must match");

            IOffer butterOffer = new ButterOffer(new TestLogger());
            IOffer milkOffer = new MilkOffer(new TestLogger());
            ProductPricer productCollectionPricer = new ProductPricer(new TestLogger());
            List<ProductInBasket> productsInBasket = new List<ProductInBasket>();

            // For each product, if this test requires it, put it into the basket with the appropriate quantity
            for (int i = 0; i < product.Length; i++)
                productsInBasket.Add(new ProductInBasket(GetProduct(product[i]), quantity[i]));

            var result = productCollectionPricer.GetPrice(productsInBasket, new IOffer[] { butterOffer,milkOffer });

            Assert.That(result, Is.EqualTo(expectedPrice));
        }

    }

    public class TestLogger : ILog
    {
        private string _lastMessage;
        public void LogMessage(string message)
        {
            _lastMessage = message;
        }

        public string GetLastMessage()
        {
            return _lastMessage;
        }
    }
}