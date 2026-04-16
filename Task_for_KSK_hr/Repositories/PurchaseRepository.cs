using Task_for_KSK_hr.Models;

namespace Task_for_KSK_hr.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly object _lock = new object();
        private readonly List<Purchase> _purchases = new List<Purchase>();
        private int _nextId = 1;

        public Purchase Get(int id)
        {
            lock (_lock)
            {
                return _purchases.FirstOrDefault(p => p.Id == id);
            }
        }
        public void Add(Purchase purchase)
        {
            lock (_lock)
            {
                purchase.Id = _nextId++;
                _purchases.Add(purchase);
            }
            
        }

        public void Update(Purchase purchase)
        {
            lock (_lock)
            {
                var current = Get(purchase.Id);
                if (current != null)
                {
                    current.Amount=purchase.Amount;
                }
            }
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                _purchases.RemoveAll(p => p.Id == id);
            }
        }

        public List<Purchase> GetAll()
        {
            lock(_lock)
            {
                return _purchases.ToList();
            }
        }
    }
}
