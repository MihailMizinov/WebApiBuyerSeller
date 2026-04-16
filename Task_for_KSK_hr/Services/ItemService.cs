using Task_for_KSK_hr.Models;
using Task_for_KSK_hr.Monitors;
using Task_for_KSK_hr.Repositories;

namespace Task_for_KSK_hr.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepo;
        private readonly ITopCategoryMonitor _monitor;
        private readonly object _itemLock = new object();

        public ItemService(IItemRepository itemRepo, ITopCategoryMonitor monitor)
        {
            _itemRepo = itemRepo;
            _monitor = monitor;
        }

        public List<Item> GetAll() => _itemRepo.GetAll();

        public Item Add(Item item)
        {
            lock (_itemLock)
            {
                item.Id = 0;
                _itemRepo.Add(item);
                _monitor.Recalculate();

                return item;
            }
        }

        public Item Update(Item item)
        {
            lock ( _itemLock)
            {
                var current = _itemRepo.Get(item.Id);
                if (current == null) 
                {
                    throw new Exception("Такого товара нет!");
                }

                _itemRepo.Update(item);
                _monitor.Recalculate();
                return item;
            }
        }

        public void Delete(int id)
        {
            lock (_itemLock)
            {
                var current = _itemRepo.Get(id);
                if (current == null) throw new Exception("Такого товара нет!");
            }
            _itemRepo.Delete(id);
            _monitor.Recalculate();
        }
    }
}
