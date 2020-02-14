using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Models;
using WebApi.DTO;
using System.Security.Claims;
using System;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PurchaseTransactionsController : ControllerBase
    {
        private readonly IPurchaseService _userService;

        public PurchaseTransactionsController(IPurchaseService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllTrans()
        {
            var purchaseTransactions = _userService.GetAllTrans();
            if (purchaseTransactions == null)
                return NoContent();

            return Ok(purchaseTransactions);
        }

        [HttpGet("{id}")]
        public IActionResult GetTrans(long id)
        {
            var purchaseTransaction = _userService.GetTrans(id);
            if (purchaseTransaction == null)
                return NotFound();

            return Ok(purchaseTransaction);
        }

        [HttpPost]
        public IActionResult CreateTrans(PurchaseTransactionDto purchaseTransaction)
        {
            var userId = Convert.ToInt64(HttpContext.User.Identity.Name);
            if (!ModelState.IsValid || userId == 0)
                return BadRequest();

            var createdTransaction = _userService.CreateTrans(userId,purchaseTransaction);
            if (createdTransaction == null)
                return BadRequest();

            return Ok(createdTransaction);
        }
    }
}
