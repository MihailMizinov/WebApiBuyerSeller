using Moq;
using Task_for_KSK_hr.Models;
using Task_for_KSK_hr.Monitors;
using Task_for_KSK_hr.Repositories;
using Task_for_KSK_hr.Services;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1_BuyCorrect()
        {
            var mockItemRepo = new Mock<IItemRepository>();
            var item = new Item { Id = 1, Name = "Ring", Category = Category.jewelry, Price = 100 };

            mockItemRepo.Setup(r => r.Get(1)).Returns(item);

            var mockPurchaseRepo = new Mock<IPurchaseRepository>();
            var mockMonitor = new Mock<ITopCategoryMonitor>();

            var service = new PurchaseService(mockItemRepo.Object,
                                              mockPurchaseRepo.Object,
                                              mockMonitor.Object);

            var purchase = service.Buy(1, 42, 3);

            Assert.Equal(1, purchase.ItemId);
            Assert.Equal(42, purchase.BuyerId);
            Assert.Equal(100, purchase.PurchasePrice);
            Assert.Equal(3, purchase.Amount);

        }


        [Fact]
        public void Test2_BuyWithUnnormalAmount()
        {
            var mockItemRepo = new Mock<IItemRepository>();
            var item = new Item { Id = 1, Name = "Ring", Category = Category.jewelry, Price = 100 };

            mockItemRepo.Setup(r => r.Get(1)).Returns(item);

            var service = new PurchaseService(
                mockItemRepo.Object, 
                Mock.Of<IPurchaseRepository>(), 
                Mock.Of<ITopCategoryMonitor>());

            Assert.Throws<Exception>(() => service.Buy(1, 1, 0));
            Assert.Throws<Exception>(() => service.Buy(1, 1, -5));
        }
    }
}
