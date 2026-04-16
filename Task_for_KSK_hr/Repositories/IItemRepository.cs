using Task_for_KSK_hr.Models;

namespace Task_for_KSK_hr.Repositories
{
    public interface IItemRepository
    {
        List<Item> GetAll();
        Item Get(int id);
        void Add(Item item);
        void Update(Item item);
        void Delete(int id);
        
    }
}
