using Moq;
using ProvaPub.Interfaces;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        [Fact(DisplayName = "CanPurchase Valid Customer And Values")]
        [Trait("CanPurchase", "CustomerService CanPurchase Trait Tests")]
        public async Task CanPurchase_ValidCustomerAndValue_ReturnsTrue()
        {
            // Arrange
            var customerService = CreateCustomerService();
            var customerId = 1;
            var purchaseValue = 50;

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "CanPurchase Invalid Customer")]
        [Trait("CanPurchase", "CustomerService CanPurchase Trait Tests")]
        public async Task CanPurchase_NegativeCustomerId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var customerService = CreateCustomerService();
            var customerId = -1;
            var purchaseValue = 50;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await customerService.CanPurchase(customerId, purchaseValue);
            });
        }

        [Fact(DisplayName = "CanPurchase Invalid Value")]
        [Trait("CanPurchase", "CustomerService CanPurchase Trait Tests")]
        public async Task CanPurchase_NegativePurchaseValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var customerService = CreateCustomerService();
            var customerId = 1;
            var purchaseValue = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await customerService.CanPurchase(customerId, purchaseValue);
            });
        }

        [Fact(DisplayName = "CanPurchase Nonexistent Customer")]
        [Trait("CanPurchase", "CustomerService CanPurchase Trait Tests")]
        public async Task CanPurchase_NonRegisteredCustomer_ReturnsFalse()
        {
            // Arrange
            var customerService = CreateCustomerService(customerExists: false);
            var customerId = 1;
            var purchaseValue = 50;

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "CanPurchase Has Existent Purchase This Month")]
        [Trait("CanPurchase", "CustomerService CanPurchase Trait Tests")]
        public async Task CanPurchase_CustomerHasMadePurchaseThisMonth_ReturnsFalse()
        {
            // Arrange
            var customerService = CreateCustomerService(ordersThisMonth: 1);
            var customerId = 1;
            var purchaseValue = 50;

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "CanPurchase Purchase Over 100 Fails")]
        [Trait("CanPurchase", "CustomerService CanPurchase Trait Tests")]
        public async Task CanPurchase_FirstPurchaseOver100_ReturnsFalse()
        {
            // Arrange
            var customerService = CreateCustomerService(firstPurchaseValue: 150);
            var customerId = 1;
            
            var purchaseValue = 150;

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.False(result);
        }

        private ICustomerService CreateCustomerService(int count = 0, bool customerExists = true, int ordersThisMonth = 0, decimal firstPurchaseValue = 50)
        {
            var dbContext = new Mock<TestDbContext>();
            var customerRepository = new Mock<ICustomerRepository>();
            var orderRepository = new Mock<IOrderRepository>();

            customerRepository.Setup(repo => repo.CustomerExists(It.IsAny<int>())).ReturnsAsync(customerExists);
            orderRepository.Setup(repo => repo.CountOrdersInThisMonth(It.IsAny<DateTime>(), It.IsAny<int>())).ReturnsAsync(ordersThisMonth);
            customerRepository.Setup(repo => repo.HaveBoughtBefore(It.IsAny<int>())).ReturnsAsync(count);

            return new CustomerService(customerRepository.Object, orderRepository.Object);
        }
    }
}
