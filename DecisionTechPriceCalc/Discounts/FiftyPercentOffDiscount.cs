﻿namespace DecisionTechPriceCalc
{
    public class FiftyPercentOffDiscount : IDiscount
    {
        private ILog _log;
        public FiftyPercentOffDiscount(ILog log)
        {
            _log = log;
        }
        public string Name => "Fifty Percent Discount";

        public decimal GetDiscount(ProductInBasket pricedProductInBasket)
        {
            var priceReduction = pricedProductInBasket.Price / 2;
            _log.LogMessage($"Discount {Name} applied to {pricedProductInBasket.Product.Name}:{pricedProductInBasket.Id} - price:{pricedProductInBasket.Price}, reduction:{priceReduction}");
            return -priceReduction;
        }
    }

}
