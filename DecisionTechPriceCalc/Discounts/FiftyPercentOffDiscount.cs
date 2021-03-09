namespace DecisionTechPriceCalc
{
    public class FiftyPercentOffDiscount : IDiscount
    {
        private ILog _log;
        private int _numberOfTimesDiscountApplies;

        public FiftyPercentOffDiscount(ILog log, int numberOfTimesDiscountApplies)
        {
            _log = log;
            _numberOfTimesDiscountApplies = numberOfTimesDiscountApplies;
        }
        public string Name => "Fifty Percent Discount";

        public decimal GetDiscount(ProductInBasket pricedProductInBasket)
        {
            decimal priceReduction = -pricedProductInBasket.Price / 2;
            decimal totalDiscount = _numberOfTimesDiscountApplies * priceReduction;
            _log.LogMessage($"Discount {Name} applied to {pricedProductInBasket.Product.Name}:{pricedProductInBasket.Id} - price:{pricedProductInBasket.Price}, reduction:{priceReduction} applied {_numberOfTimesDiscountApplies}, resulting in {totalDiscount }");
            return totalDiscount;
        }
    }

}
