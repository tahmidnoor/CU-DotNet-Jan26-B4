namespace GlobalMart.Services
{
    public class PricingService : IPricingService
    {
        public decimal CalculatePrice(decimal basePrice, string promoCode)
        {
            decimal finalPrice = basePrice;

            if (promoCode == "WINTER25")
            {
                finalPrice = basePrice * 0.85m; 
            }
            else if (promoCode == "FREESHIP")
            {
                finalPrice = basePrice - 5;
            }

            return finalPrice;
        }
    }
}