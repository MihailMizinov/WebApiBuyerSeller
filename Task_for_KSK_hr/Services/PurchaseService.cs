using System.Security.Cryptography.X509Certificates;
using Task_for_KSK_hr.Models;
using Task_for_KSK_hr.Monitors;
using Task_for_KSK_hr.Repositories;
using System.Linq;

namespace Task_for_KSK_hr.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IItemRepository _itemRepo;
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly ITopCategoryMonitor _monitor;

        private readonly object _buyLock = new object();


        public PurchaseService(IItemRepository itemRepo, IPurchaseRepository purchaseRepo, ITopCategoryMonitor monitor)
        {
            _itemRepo = itemRepo;
            _purchaseRepo = purchaseRepo;
            _monitor = monitor;
        }

        public List<Purchase> GetPurchasesForBuyer(int buyerId)
        {
            return _purchaseRepo.GetAll().Where(p => p.BuyerId == buyerId).ToList();
        }

        public Purchase Buy(int itemId, int buyerId, int amount)
        {
            lock (_buyLock)
            {
                var item = _itemRepo.Get(itemId);
                if (item == null) throw new Exception("Ошибка! Товар отсутствует");
                if (amount <= 0) throw new Exception("Ошибка! Попытка купить меньше 1 товара");

                var purchase = new Purchase
                {
                    ItemId = itemId,
                    BuyerId = buyerId,
                    PurchasedAt = DateTime.UtcNow,
                    PurchasePrice = item.Price,
                    Amount = amount
                };

                _purchaseRepo.Add(purchase);

                _monitor.Recalculate();

                return purchase;
            }


        }

        public void UpdatePurchaseAmount(int purchaseId, int newAmount)
        {
            lock (_buyLock)
            {
                var purchase = _purchaseRepo.Get(purchaseId);
                if (purchase == null) throw new Exception("Покупка не найдена!");
                if(newAmount <= 0) throw new Exception("Нельзя купить ничто!");

                purchase.Amount = newAmount;
                _purchaseRepo.Update(purchase);
                _monitor.Recalculate();

            }
        }
        public void CancelPurchase(int purchaseId)
        {
            lock (_buyLock)
            {
                var purchase = _purchaseRepo.Get(purchaseId);

                if (purchase == null) throw new Exception("Покупка не найдена");

                _purchaseRepo.Delete(purchaseId);
                _monitor.Recalculate();
            }
        }
    }
}
