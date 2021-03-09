using System.Collections.Generic;

namespace DecisionTechPriceCalc
{
    public interface IOffer
    {
        IEnumerable<ProductInBasket> ApplyOffer(IEnumerable<ProductInBasket> productsInBasket);
    }
}
