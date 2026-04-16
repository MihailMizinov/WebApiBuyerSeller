using Task_for_KSK_hr.Models;

namespace Task_for_KSK_hr.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly List<Item> _items = new List<Item>();
        private readonly object _lock = new object();
        private int _nextId = 1;

        public List<Item> GetAll()
        {
            lock (_lock)
            {
                return _items.ToList();
            }

        }

        public Item Get(int id)
        {
            lock (_lock)
            {
                return _items.FirstOrDefault(i => i.Id == id);
            }
        }

        public void Add(Item item)
        {
            lock (_lock)
            {
                item.Id = _nextId++;
                _items.Add(item);
            }
        }

        public void Update(Item item)
        {
            lock ( _lock)
            {
                var current = Get(item.Id);
                if (current != null)
                {
                    current.Name = item.Name;
                    current.Category = item.Category;
                    current.Price = item.Price;
                    current.SellerId = item.SellerId;
                }
            }
        }

        public void Delete(int id)
        {
            lock(_lock)
            {
                _items.RemoveAll(i => i.Id == id);
            }
        }
    }
}
