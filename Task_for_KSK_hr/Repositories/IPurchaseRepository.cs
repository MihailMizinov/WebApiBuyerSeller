using Task_for_KSK_hr.Models;

namespace Task_for_KSK_hr.Repositories
{
    public interface IPurchaseRepository
    {
        Purchase Get(int id);
        void Add(Purchase purchase);
        void Update(Purchase purchase);
        void Delete(int id);
        List<Purchase> GetAll();
    }
}
