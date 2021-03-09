using System.Collections.Generic;
using System.Linq;

namespace DecisionTechPriceCalc
{
    public class OfferApplier
    {
        public static IEnumerable<ProductInBasket> ApplyDiscountToProduct(IEnumerable<ProductInBasket> productsInBasket, string productName, IDiscount discount)
        {
            List<ProductInBasket> updatedList = new List<ProductInBasket>();
            var idsToApplyDiscountTo =  productsInBasket.Where(product => product.Product.Name ==  productName).Select(product => product.Id).ToList();
            foreach (ProductInBasket pricedProductInBasket in productsInBasket)
            {
                if (idsToApplyDiscountTo.Contains(pricedProductInBasket.Id))
                    pricedProductInBasket.AddDiscount(discount);
                updatedList.Add(pricedProductInBasket);
            }
            return updatedList;
        }
    }
}
