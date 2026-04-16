using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Task_for_KSK_hr.Models;
using Task_for_KSK_hr.Repositories;

namespace Task_for_KSK_hr.Monitors
{
    public class TopCategoryMonitor : ITopCategoryMonitor
    {
        private TopCategoryOptions _currentValue;
        private readonly IItemRepository _itemRepo;
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly List<Action<TopCategoryOptions, string>> _listeners = new List<Action<TopCategoryOptions, string>>();

        public TopCategoryMonitor(IItemRepository itemRepo, IPurchaseRepository purchaseRepo)
        {
            _itemRepo = itemRepo;
            _purchaseRepo = purchaseRepo;
            Recalculate();
        }

        public TopCategoryOptions CurrentValue => _currentValue;

        public void Recalculate()
        {
            var purchases = _purchaseRepo.GetAll();
            var items = _itemRepo.GetAll();

            var revenueByCategory = new Dictionary<Category, decimal>();


            foreach ( var purchase in purchases )
            {
                var item = items.FirstOrDefault(i => i.Id == purchase.ItemId);
                if ( item != null )
                {
                    if (!revenueByCategory.ContainsKey(item.Category))
                        revenueByCategory[item.Category] = 0;
                    revenueByCategory[item.Category] += (purchase.PurchasePrice * purchase.Amount);

                
                }
            }

            if (revenueByCategory.Count == 0)
            {
                _currentValue = new TopCategoryOptions { topCategory = "None", AveragePriceTopCategory = 0 };
            }
            else
            {
                var topCategory = revenueByCategory.OrderByDescending(kv => kv.Value).First().Key;

                var avgPrice = items.Where(i => i.Category == topCategory).Select(i => i.Price).DefaultIfEmpty(0).Average();
                _currentValue = new TopCategoryOptions
                {
                    topCategory = topCategory.ToString(),
                    AveragePriceTopCategory = avgPrice
                };
            }

            foreach(var listener in _listeners)
            {
                listener(_currentValue, null);
            }

        }

        public IDisposable OnChange(Action<TopCategoryOptions, string> listener)
        {
            _listeners.Add(listener);
            return new Disposable(() => _listeners.Remove(listener));
        }

        public TopCategoryOptions Get(string name)
        {
            return _currentValue;
        }

        private class Disposable : IDisposable
        {
            private readonly Action _action;
            public Disposable(Action action) => _action = action;
            public void Dispose() => _action();
        }
    }
}
