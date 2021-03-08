using System.Collections.Generic;

namespace DecisionTechPriceCalc
{
    public interface IOffer
    {
        IEnumerable<ProductInBasket> ApplyOffer(IEnumerable<ProductInBasket> productsInBasket);
    }

    public class OfferDetails
    {
        public string OfferName { get; }
        public string OfferDescription { get; }
    }
}
