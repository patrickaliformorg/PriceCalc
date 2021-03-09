using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTechPriceCalc
{
    public class NthItemDiscount : IOffer
    {
        private ILog _log;
        private int _nthItemFree;
        private string _productName;
        private Func<int, IDiscount> _discounter;

        public NthItemDiscount(ILog log, string productName, int nthItemFree,Func<int, IDiscount> discounter)
        {
            _log = log;
            _nthItemFree = nthItemFree;
            _productName = productName;
            _discounter = discounter;
        }

        public IEnumerable<ProductInBasket> ApplyOffer(IEnumerable<ProductInBasket> productsInBasket)
        {
            var totalNumberOfProducts = productsInBasket.Where(product => product.Product.Name == _productName).Select(product => product.Quantity).Sum();
            var fourthMilkHasDiscount = totalNumberOfProducts / _nthItemFree;
            return OfferApplier.ApplyDiscountToProduct(productsInBasket, _productName, _discounter(fourthMilkHasDiscount));
        }
    }


}
