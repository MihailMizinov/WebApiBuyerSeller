using Task_for_KSK_hr.Models;

namespace Task_for_KSK_hr.Services
{
    public interface IItemService
    {
        List<Item> GetAll();
        Item Add(Item item);
        Item Update(Item item);
        void Delete(int id);
    }
}
