using Microsoft.AspNetCore.Mvc;
using Task_for_KSK_hr.Models;
using Task_for_KSK_hr.Services;

namespace Task_for_KSK_hr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly IItemService _itemService;
        public SellerController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("items")] public IActionResult GetAll()
        {
            return Ok(_itemService.GetAll());
        }
        [HttpPost("item")] public IActionResult Add(Item item)
        {
            return Ok(_itemService.Add(item));
        }
        [HttpPut("item")]
        public IActionResult Update(Item item)
        {
            return Ok(_itemService.Update(item));
        }

        [HttpDelete("item/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
