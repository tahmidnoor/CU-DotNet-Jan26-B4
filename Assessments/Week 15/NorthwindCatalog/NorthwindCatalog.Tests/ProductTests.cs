using Xunit;
using NorthwindCatalog.Services.DTOs;

namespace NorthwindCatalog.Tests
{
    public class ProductTests
    {
        [Fact]
        public void InventoryValue_Should_Return_Correct_Value()
        {
            var product = new ProductDto
            {
                UnitPrice = 50,
                UnitsInStock = 4
            };

            var result = product.InventoryValue;

            Assert.Equal(200, result);
        }
    }
}