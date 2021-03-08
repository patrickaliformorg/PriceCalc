namespace DecisionTechPriceCalc
{
    public interface IDiscount
    {
        string Name { get; }
        decimal GetDiscount(ProductInBasket pricedProductInBasket);
    }


}
