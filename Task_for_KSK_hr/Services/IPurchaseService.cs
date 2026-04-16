using Task_for_KSK_hr.Models;

namespace Task_for_KSK_hr.Services
{
    public interface IPurchaseService
    {
        public List<Purchase> GetPurchasesForBuyer(int buyerId);
        Purchase Buy(int itemId, int buyerId, int amount);

        void UpdatePurchaseAmount(int purchaseId, int newAmount);
        void CancelPurchase(int purchaseId);
    }
}
