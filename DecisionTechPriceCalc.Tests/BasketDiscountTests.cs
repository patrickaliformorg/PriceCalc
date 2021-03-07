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

        //        • Given the basket has 1 bread, 1 butter and 1 milk when I total the basket then the total
        //should be £2.95
        //• Given the basket has 2 butter and 2 bread when I total the basket then the total should be
        //£3.10
        //• Given the basket has 4 milk when I total the basket then the total should be £3.45
        //• Given the basket has 2 butter, 1 bread and 8 milk when I total the basket then the total
        //should be £9.00
        [TestCase(new string[] { "Bread", "Butter", "Milk" }, new int[] { 1, 1, 1 }, 2.95, TestName = "1 Bread, 1 Butter, 1 Milk")]
//     [TestCase(new string[] { "Bread", "Butter" }, new int[] { 2, 2 }, 3.10, TestName = "2 Bread, 2 Butter")]
        //       [TestCase(new string[]{"Milk"},new int[]{4},3.45,TestName ="4 Milk")]
        //      [TestCase(new string[]{"Bread","Butter","Milk"},new int[]{1,2,8},2.95,TestName ="1 Bread, 1 Butter, 1 Milk")]
        public void GivenABasketOfProductsAnExpectedPrice(string[] product, int[] quantity, decimal expectedPrice)
        {
            if(product.Length != quantity.Length)
                throw new System.ArgumentException("Products and Quantity arrays must match");

            ProductCollectionPricer productCollectionPricer = new ProductCollectionPricer();
            List<ProductInBasket> productsInBasket = new List<ProductInBasket>();

            // For each product, if this test requires it, put it into the basket with the appropriate quantity
            for (int i = 0; i < product.Length; i++)
                productsInBasket.Add(new ProductInBasket(GetProduct(product[i]), quantity[i]));

            var result = productCollectionPricer.GetPrice(productsInBasket);

            Assert.That(result, Is.EqualTo(expectedPrice));
        }
    }


}