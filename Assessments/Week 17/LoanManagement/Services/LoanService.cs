using Serilog;
using LoanManagement.DTOs;
using LoanManagement.Repository;
using LoanManagement.Models;

namespace LoanManagement.Services
{
    public class LoanService
    {
        private readonly LoanRepo _repo;

        public LoanService(LoanRepo repo)
        {
            _repo = repo;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<Loan>> GetAllService()
        {
            Log.Information("Fetching all loans");

            var resp = await _repo.GetAllAsync();

            if (resp == null || !resp.Any())
            {
                Log.Warning("No loans found in database");
                throw new Exception("No loans found.");
            }

            Log.Information("Fetched {Count} loans", resp.Count);
            return resp;
        }

        // =========================
        // GET BY USER
        // =========================
        public async Task<List<Loan>> GetByUserService(string userId)
        {
            Log.Information("Fetching loans for UserId: {UserId}", userId);

            var resp = await _repo.GetByUserAsync(userId);

            if (resp == null || !resp.Any())
            {
                Log.Warning("No loans found for UserId: {UserId}", userId);
                throw new Exception($"No loans found by user:{userId}.");
            }

            return resp;
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<Loan> GetByIdService(int id)
        {
            Log.Information("Fetching loan by Id: {LoanId}", id);

            var resp = await _repo.GetByIdAsync(id);

            if (resp == null)
            {
                Log.Warning("Loan not found for Id: {LoanId}", id);
                throw new Exception($"No loans found by id:{id}");
            }

            return resp;
        }

        // =========================
        // CREATE
        // =========================
        public async Task<LoanDto> CreateService(CreateLoanDto loan)
        {
            Log.Information("Creating loan for UserId: {UserId}", loan.UserId);

            if (loan.Amount <= 0)
            {
                Log.Warning("Invalid loan amount: {Amount}", loan.Amount);
                throw new ArgumentException("Loan amount must be greater than 0");
            }

            var resp = await _repo.CreateAsync(loan);

            if (resp == null)
            {
                Log.Error("Failed to create loan for UserId: {UserId}", loan.UserId);
                throw new Exception("Unable to create loan.");
            }

            Log.Information("Loan created successfully for UserId: {UserId}", loan.UserId);

            var result = new LoanDto
            {
                UserId = resp.UserId,
                Amount = resp.Amount,
                DocsVerified = resp.DocsVerified,
                AdminReviewStatus = resp.AdminReviewStatus,
                AdminReviewReason = resp.AdminReviewReason,
                AdminReviewedAt = resp.AdminReviewedAt,
                ManagerReviewStatus = resp.ManagerReviewStatus,
                ManagerReviewReason = resp.ManagerReviewReason,
                ManagerReviewedAt = resp.ManagerReviewedAt,
                LoanType = resp.LoanType,
                Purpose = resp.Purpose,
                Status = resp.Status,
                TermInMonths = resp.TermInMonths,
            };

            return result;
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Loan> UpdateService(int id, Loan loan)
        {
            Log.Information("Updating loan Id: {LoanId}", id);

            var newLoan = new LoanDto
            {
                Amount = loan.Amount,
                AppliedAt = loan.AppliedAt,
                DocsVerified = loan.DocsVerified,
                AdminReviewStatus = loan.AdminReviewStatus,
                AdminReviewReason = loan.AdminReviewReason,
                AdminReviewedAt = loan.AdminReviewedAt,
                ManagerReviewStatus = loan.ManagerReviewStatus,
                ManagerReviewReason = loan.ManagerReviewReason,
                ManagerReviewedAt = loan.ManagerReviewedAt,
                LoanType = loan.LoanType,
                UserId = loan.UserId,
                Purpose = loan.Purpose,
                Status = loan.Status,
                TermInMonths = loan.TermInMonths,
                UpdatedAt = loan.UpdatedAt,
            };

            var resp = await _repo.UpdateAsync(id, newLoan);

            if (resp == null)
            {
                Log.Error("Failed to update loan Id: {LoanId}", id);
                throw new Exception("Unable to update.");
            }

            Log.Information("Loan updated successfully Id: {LoanId}", id);
            return resp;
        }

        // =========================
        // DELETE
        // =========================
        public async Task<LoanDto> DeleteService(int id)
        {
            Log.Information("Deleting loan Id: {LoanId}", id);

            var resp = await _repo.DeleteAsync(id);

            if (resp == null)
            {
                Log.Error("Failed to delete loan Id: {LoanId}", id);
                throw new Exception("Unable to delete Loan.");
            }

            Log.Information("Loan deleted successfully Id: {LoanId}", id);

            var result = new LoanDto
            {
                UserId = resp.UserId,
                Amount = resp.Amount,
                AppliedAt = resp.AppliedAt,
                DocsVerified = resp.DocsVerified,
                AdminReviewStatus = resp.AdminReviewStatus,
                AdminReviewReason = resp.AdminReviewReason,
                AdminReviewedAt = resp.AdminReviewedAt,
                ManagerReviewStatus = resp.ManagerReviewStatus,
                ManagerReviewReason = resp.ManagerReviewReason,
                ManagerReviewedAt = resp.ManagerReviewedAt,
                LoanType = resp.LoanType,
                Purpose = resp.Purpose,
                Status = resp.Status,
                TermInMonths = resp.TermInMonths,
                UpdatedAt = resp.UpdatedAt,
            };

            return result;
        }

        // =========================
        // UPDATE DECISION
        // =========================
        public async Task<Loan> UpdateDecisionService(int id, string reviewerRole, string status, string? reason)
        {
            Log.Information("Review started for LoanId: {LoanId} by {Role}", id, reviewerRole);

            var resp = await _repo.UpdateDecisionAsync(id, reviewerRole, status, reason);

            if (resp == null)
            {
                Log.Error("Failed to update decision for LoanId: {LoanId}", id);
                throw new Exception("Unable to update loan decision.");
            }

            if (status == "Rejected")
            {
                Log.Warning("Loan {LoanId} rejected by {Role}. Reason: {Reason}", id, reviewerRole, reason);
            }
            else
            {
                Log.Information("Loan {LoanId} approved by {Role}", id, reviewerRole);
            }

            return resp;
        }

        // =========================
        // PAY EMI
        // =========================
        public async Task<Loan?> PayEmiService(int emiId)
        {
            Log.Information("Processing EMI payment for EMI ID: {EmiId}", emiId);

            var resp = await _repo.PayEmiAsync(emiId);

            if (resp == null)
            {
                Log.Warning("EMI payment failed or invalid EMI ID: {EmiId}", emiId);
                return null;
            }

            Log.Information("EMI payment successful for EMI ID: {EmiId}", emiId);
            return resp;
        }
    }
}