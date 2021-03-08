using System.Collections.Generic;
using System.Linq;

namespace DecisionTechPriceCalc
{
    public class MilkOffer : IOffer
    {
        private ILog _log;
        public MilkOffer(ILog log)
        {
            _log = log;
        }

        public IEnumerable<ProductInBasket> ApplyOffer(IEnumerable<ProductInBasket> productsInBasket)
        {

            List<ProductInBasket> updatedList = new List<ProductInBasket>();
            var totalMilks = productsInBasket.Where(product => product.Product.Name == "Milk").Select(product => product.Quantity).Sum();
            var fourthMilkHasDiscount = totalMilks / 4;
            var milkDiscountsIds = productsInBasket.Where(product => product.Product.Name == "Milk").Select(x => x.Id).ToList();
            foreach (ProductInBasket pricedProductInBasket in productsInBasket)
            {
                if (milkDiscountsIds.Contains(pricedProductInBasket.Id))
                {
                    for (int i = 0; i < fourthMilkHasDiscount; i++)
                        pricedProductInBasket.AddDiscount(new FreeItemDiscount(_log));
                }
                updatedList.Add(pricedProductInBasket);
            }
            return updatedList;
        }
    }


}
