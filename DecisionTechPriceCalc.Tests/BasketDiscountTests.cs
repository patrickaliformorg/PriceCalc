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

            Basket basket = new Basket(new IOffer[] { });

            // For each product, if this test requires it, put it into the basket with the appropriate quantity
            for (int i = 0; i < product.Length; i++)
                basket.AddProductToBasket(GetProduct(product[i]), quantity[i]);

            var result = basket.GetBasketTotalPrice();
            Assert.That(result, Is.EqualTo(expectedPrice));
        }

        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 1, 1, 1 }, 2.95, TestName = "2 Offers - 1 Bread, 1 Butter, 1 Milk")]
        [TestCase(new string[] { "Bread", "Butter" }, new int[] { 2, 2 }, 3.10, TestName = "2 Offers - 2 Bread, 2 Butter")]
        [TestCase(new string[] { "Milk" }, new int[] { 4 }, 3.45, TestName = "2 Offers - 4 Milk")]
        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 1, 2, 8 }, 9, TestName = "2 Offers - 1 Bread, 2 Butter, 8 Milk")]
        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 15, 17, 29 }, 49.9, TestName = "2 Offers - 15 Bread, 17 Butter, 29 Milk")]
        [TestCase(new string[] { "Bread", "Butter" }, new int[] { 6, 52 }, 44.6, TestName = "2 Offers - Large quantity  of Butter, small quantity Bread -  6 Bread, 52 Butter")]
        [TestCase(new string[] { "Bread", "Butter" }, new int[] { 23, 5 }, 26, TestName = "2 Offers - Large quantity of Bread, small quantity of butter-  23 Bread, 5 Butter")]
        public void GivenABasketWithTwoOffers(string[] product, int[] quantity, decimal expectedPrice)
        {
            if (product.Length != quantity.Length)
                throw new System.ArgumentException("Products and Quantity arrays must match");

            TestLogger butterOfferLogger = new TestLogger();
            TestLogger milkOfferLogger = new TestLogger();

            IOffer butterOffer =
                new NItemsOfFirstProductDeterminesDiscountOnSecond(butterOfferLogger,
                "Butter",
                "Bread",
                (noOfDiscounts) => new FiftyPercentOffDiscount(butterOfferLogger, noOfDiscounts));

            IOffer milkOffer = 
                new NthItemDiscount(milkOfferLogger,
                "Milk",
                4,
                (noOfDiscounts) => new FreeItemDiscount(milkOfferLogger, noOfDiscounts));

            Basket basket = new Basket(new IOffer[] { butterOffer, milkOffer });

            // For each product, if this test requires it, put it into the basket with the appropriate quantity
            for (int i = 0; i < product.Length; i++)
                basket.AddProductToBasket(GetProduct(product[i]), quantity[i]);

            var result = basket.GetBasketTotalPrice();

            Assert.That(result, Is.EqualTo(expectedPrice));
        }

        // **************************************************************************
        // Further tests to write, tests to assert that the expected logs are written
        // Tests for some edge cases, such as a negative quantity of items
        // Tests for the results with just one offer

    }

    public class TestLogger : ILog
    {
        private string _lastMessage = "";
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