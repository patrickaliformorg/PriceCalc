using System;
using System.Collections.Generic;

namespace DecisionTechPriceCalc
{
    public class ProductInBasket
    {
        public ProductInBasket(Product product, int quantity)
        {
            this.Id = Guid.NewGuid();
            this.Product = product;
            this.Quantity = quantity;
            this.Discounts = new List<IDiscount>();
        }
        public Product Product { get; }
        public Guid Id { get; }
        public decimal Price => Product.Price;
        public int Quantity { get; }
        public List<IDiscount> Discounts { get; }
        public void AddDiscount(IDiscount discount)
        {
            Discounts.Add(discount);
        }
    }


}
