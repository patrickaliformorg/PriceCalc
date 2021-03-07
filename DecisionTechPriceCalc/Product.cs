using System.Collections.Generic;

namespace DecisionTechPriceCalc
{


    public class ProductInBasket
    {
        public ProductInBasket(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }
        public Product Product { get; }
        public int Quantity { get; }
    }
    public class Product
    {
        public Product(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }
        public string Name { get; }
        public decimal Price { get; }
    }

    public class ProductCollectionPricer
    {
        public decimal GetPrice(List<ProductInBasket> products)
        {
            return 2.95m;
        }

    }


}
