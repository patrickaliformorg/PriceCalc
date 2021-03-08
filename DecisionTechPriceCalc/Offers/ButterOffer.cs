﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTechPriceCalc
{

    public class ButterOffer : IOffer
    {
        private ILog _log;
        public ButterOffer(ILog log)
        {
            _log = log;
        }

        /// <summary>
        /// Receives collection of ProductsInBasket and returns the List with any necessary Discounts added
        /// </summary>
        public IEnumerable<ProductInBasket> ApplyOffer(IEnumerable<ProductInBasket> productsInBasket)
        {
            List<ProductInBasket> updatedList = new List<ProductInBasket>();

            List<Guid> breadDiscountsIds = GetProductIdsToApplyDiscount(productsInBasket);
            foreach (ProductInBasket pricedProductInBasket in productsInBasket)
            {
                if (breadDiscountsIds.Contains(pricedProductInBasket.Id))
                    pricedProductInBasket.AddDiscount(new FiftyPercentOffDiscount(_log));
                updatedList.Add(pricedProductInBasket);
            }
            return updatedList;
        }

        private List<Guid> GetProductIdsToApplyDiscount(IEnumerable<ProductInBasket> productsInBasket)
        {
            int totalButters = productsInBasket.Where(product => product.Product.Name == "Butter").Select(product => product.Quantity).Sum();
            int numberOfBreadsToBe50PercentOff = totalButters / 2;
            return productsInBasket.Where(product => product.Product.Name == "Bread").Take(numberOfBreadsToBe50PercentOff).Select(product => product.Id).ToList();
        }

    }


}