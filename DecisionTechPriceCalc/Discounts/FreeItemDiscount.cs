namespace DecisionTechPriceCalc
{
    public class FreeItemDiscount : IDiscount
    {
        private ILog _log;
        private int _numberOfTimesDiscountApplies;
        public FreeItemDiscount(ILog log, int numberOfTimesDiscountApplies )
        {
            _log = log;
            _numberOfTimesDiscountApplies = numberOfTimesDiscountApplies;
        }
        public string Name => "Free Item Discount";

        public decimal GetDiscount(ProductInBasket pricedProductInBasket)
        {
            decimal priceReduction = -pricedProductInBasket.Price;
            decimal totalDiscount = _numberOfTimesDiscountApplies*priceReduction;
            _log.LogMessage($"Discount {Name} applied to {pricedProductInBasket.Product.Name}:{pricedProductInBasket.Id} - price:{pricedProductInBasket.Price}, reduction:{priceReduction} applied {_numberOfTimesDiscountApplies}, resulting in {totalDiscount }");
            return totalDiscount ;
        }

    }
}
