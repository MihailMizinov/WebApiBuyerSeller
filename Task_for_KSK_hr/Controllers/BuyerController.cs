using Microsoft.AspNetCore.Mvc;
using Task_for_KSK_hr.Monitors;
using Task_for_KSK_hr.Repositories;
using Task_for_KSK_hr.Services;

namespace Task_for_KSK_hr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyerController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IItemService _itemService;

        public BuyerController(IPurchaseService purchaseService, IItemService itemService)
        {
            _purchaseService = purchaseService;
            _itemService = itemService;
        }

        [HttpPost("buy/{itemId}")]
        public IActionResult Buy(int itemId, int buyerId, int amount = 1)
        {
            try
            {
                return Ok(_purchaseService.Buy(itemId, buyerId, amount));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("items")]
        public IActionResult GetItems()
        {
            return Ok(_itemService.GetAll());
        }


        [HttpGet("purchases")]
        public IActionResult GetPurchases(int buyerId)
        {
            return Ok(_purchaseService.GetPurchasesForBuyer(buyerId));
        }

        [HttpPut("purchase/{purchaseId}")]
        public IActionResult UpdatePurchaseAmount(int purchaseId, int newAmount)
        {
            try
            {
                _purchaseService.UpdatePurchaseAmount(purchaseId, newAmount);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("purchase/{purchaseId}")]
        public IActionResult DeletePurchase(int purchaseId)
        {
            try
            {
                _purchaseService.CancelPurchase(purchaseId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
