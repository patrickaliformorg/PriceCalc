using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTechPriceCalc
{

    public class NItemsOfFirstProductDeterminesDiscountOnSecond : IOffer
    {
        private ILog _log;
        private string _firstProduct;
        private string _secondProduct;
        private Func<int, IDiscount> _discounter;

        public NItemsOfFirstProductDeterminesDiscountOnSecond(ILog log, string firstProduct, string secondProduct, Func<int, IDiscount> discounter)
        {
            _log = log;
            _firstProduct = firstProduct;
            _secondProduct = secondProduct;
            _discounter = discounter;
        }

        public IEnumerable<ProductInBasket> ApplyOffer(IEnumerable<ProductInBasket> productsInBasket)
        {
            int totalNumberOfFirstProduct = productsInBasket.Where(product => product.Product.Name == _firstProduct).Select(product => product.Quantity).Sum();
            int totalNumberOfSecondProduct = productsInBasket.Where(product => product.Product.Name == _secondProduct).Select(product => product.Quantity).Sum();

            int numberOfDiscountsPossible = totalNumberOfFirstProduct / 2;

            int totalNumberOfDiscountsThatCanBeApplied = Math.Min(numberOfDiscountsPossible, totalNumberOfSecondProduct);

            return OfferApplier.ApplyDiscountToProduct(productsInBasket, _secondProduct, _discounter(totalNumberOfDiscountsThatCanBeApplied));
        }


    }


}
