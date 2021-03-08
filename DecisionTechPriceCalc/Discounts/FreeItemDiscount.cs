namespace DecisionTechPriceCalc
{
    public class FreeItemDiscount : IDiscount
    {
        private ILog _log;
        public FreeItemDiscount(ILog log)
        {
            _log = log;
        }
        public string Name => "Free Item Discount";

        public decimal GetDiscount(ProductInBasket pricedProductInBasket)
        {
            decimal priceReduction = -pricedProductInBasket.Price;
            _log.LogMessage($"Discount {Name} applied to {pricedProductInBasket.Product.Name}:{pricedProductInBasket.Id} - price:{pricedProductInBasket.Price}, reduction:{priceReduction}");
            return priceReduction;
        }

    }
}
