using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTechPriceCalc
{
    public class Basket
    {

        private List<ProductInBasket> ProductsInBasket { get; }

        private IOffer[] _offers;
        public Basket(IOffer[] offers)
        {
            _offers = offers;
            this.ProductsInBasket = new List<ProductInBasket>();
        }

        public void AddProductToBasket(Product product, int quantity)
        {
            this.ProductsInBasket.Add(new ProductInBasket(product, quantity));
        }


        public decimal GetBasketTotalPrice()
        {
            return ProductPricer.GetPrice(this.ProductsInBasket, _offers);
        }
    }
}
