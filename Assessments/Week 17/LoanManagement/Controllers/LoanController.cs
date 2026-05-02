using LoanManagement.DTOs;
using LoanManagement.Services;
using LoanManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LoanManagement.Controllers
{
    [ApiController]
    [Route("loan")]
    [Authorize] 
    public class LoanController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoanController(LoanService loanService)
        {
            _loanService = loanService;
        }

        // 🔹 GET ALL LOANS (Admin / Manager)
        [HttpGet("getall")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAll()
        {
            var resp = await _loanService.GetAllService();
            return Ok(resp);
        }

        // 🔹 GET LOANS BY USER (Customer can see own, Admin/Manager can see any)
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,Manager,Customer")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var loggedUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // 🔐 Customer can only access their own data
            if (role == "Customer" && loggedUserId != userId)
            {
                return Forbid("You can only view your own loans");
            }

            var resp = await _loanService.GetByUserService(userId);
            return Ok(resp);
        }

        // 🔹 GET LOAN BY ID (All authenticated users)
        [HttpGet("getbyid/{id}")]
        [Authorize(Roles = "Admin,Manager,Customer")]
        public async Task<IActionResult> GetById(int id)
        {
            var resp = await _loanService.GetByIdService(id);

            if (resp == null)
                return NotFound("Loan not found");

            return Ok(resp);
        }

        // 🔹 UPDATE LOAN (Admin / Manager only)
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Update(int id, Loan loan)
        {
            if (id != loan.LoanId)
            {
                return BadRequest("Id mismatch");
            }

            var resp = await _loanService.UpdateService(id, loan);
            return Ok(resp);
        }

        // 🔹 CREATE LOAN
        [HttpPost("create")]
        [Authorize(Roles = "Customer,Admin,Manager")]
        public async Task<IActionResult> Create([FromBody] CreateLoanDto loan)
        {
            // 🔐 Attach logged-in user automatically
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            loan.UserId = userId;

            var resp = await _loanService.CreateService(loan);
            return Ok(resp);
        }

        // 🔹 APPROVAL / REJECTION (Admin / Manager only)
        [HttpPost("{id}/decision")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateDecision(int id, [FromBody] UpdateLoanDecisionDto dto)
        {
            var resp = await _loanService.UpdateDecisionService(
                id,
                dto.ReviewerRole,
                dto.Status,
                dto.Reason
            );

            return Ok(resp);
        }

        // 🔹 DELETE LOAN (Manager only)
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _loanService.DeleteService(id);
            return Ok(res);
        }

        // 🔥 EMI PAYMENT (Customer only)
        [HttpPost("pay-emi/{emiId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PayEmi(int emiId)
        {
            var loan = await _loanService.PayEmiService(emiId);

            if (loan == null)
            {
                return BadRequest(new
                {
                    message = "Invalid EMI or already paid"
                });
            }

            return Ok(new
            {
                message = "Payment successful",
                loan = loan
            });
        }
    }
}