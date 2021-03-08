﻿using System.Collections.Generic;
using System.Linq;

namespace DecisionTechPriceCalc
{
    public class ProductPricer
    {
        private readonly ILog _logger;
        public ProductPricer(ILog logger)
        {
            _logger = logger;
        }

        public decimal GetPrice(List<ProductInBasket> productInBasket,IOffer[] offers)
        {
            var totalPriceBeforeDiscounts = productInBasket .Select(product => GetTotalPrice(product)).Sum();

            foreach (var offer in offers)
                offer.ApplyOffer(productInBasket);

            var totalDiscount = productInBasket.Select(product => GetTotalDiscount(product)).Sum();
            // _logger.LogMessage("Log details of originalPrice and discount here")
            return totalPriceBeforeDiscounts + totalDiscount;
        }


        private decimal GetTotalDiscount(ProductInBasket productInBasket)
        {
            // Create a list of the discounts
            List<decimal> discountedItems = productInBasket.Discounts.Select(discount => discount.GetDiscount(productInBasket)).ToList();
            // Sum all the discounts
            return  discountedItems.Sum();
        }

        private  decimal GetTotalPrice(ProductInBasket productInBasket)
        {
            return productInBasket.Price * productInBasket.Quantity;       
        }
    }
}
