using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBank.TransactionService.DTOs;
using SmartBank.TransactionService.Services;

namespace SmartBank.TransactionService.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _service;

        public TransactionController(ITransactionServices service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateDTO dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetByAccountId(int accountId)
        {
            var result = await _service.GetByAccountId(accountId);
            return Ok(result);
        }
    }
}