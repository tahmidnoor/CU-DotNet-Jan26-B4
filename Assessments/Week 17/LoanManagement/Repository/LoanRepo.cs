using LoanManagement.Data;
using LoanManagement.DTOs;
using LoanManagement.Models;
using LoanManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Repository
{
    public class LoanRepo
    {
        private readonly AppDbContext _context;
        private readonly EMIService _emi;
        public LoanRepo(AppDbContext context , EMIService emi) 
        {
            _context = context;
            _emi = emi;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            var result = await _context.Loans
                .Include(l => l.EMISchedules)
                .OrderByDescending(l => l.AppliedAt)
                .ToListAsync();
            return result;
        }

        public async Task<Loan?> GetByIdAsync(int Id)
        {
            var result = await _context.Loans
                .Include(l => l.EMISchedules)
                .FirstOrDefaultAsync(l => l.LoanId == Id);
            return result;
        }

        public async Task<List<Loan>> GetByUserAsync(string userId)
        {
            return await _context.Loans
                .Include(l => l.EMISchedules)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.AppliedAt)
                .ToListAsync();
        }

        public async Task<Loan> CreateAsync(CreateLoanDto loan)
        {
            if (loan == null)
                throw new ArgumentNullException(nameof(loan));

            var newLoan = new Loan
            {
                LoanType = loan.LoanType,
                UserId = loan.UserId,
                Amount = loan.Amount,
                DocsVerified = loan.DocsVerified ?? "Pending",
                AdminReviewStatus = "Pending",
                ManagerReviewStatus = "Pending",
                Purpose = loan.Purpose,
                Status = "Pending",
                TermInMonths = loan.TermInMonths,
                AppliedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

       
            var type = loan.LoanType?.ToLower();

            if (type == "education")
            {
                newLoan.InterestRate = 12;
            }
            else if (type == "house")
            {
                newLoan.InterestRate = 7;
            }
            else
            {
                newLoan.InterestRate = 10;
            }

            await _context.Loans.AddAsync(newLoan);
            await _context.SaveChangesAsync();

            var (emi, totalPayable) = await _emi.CalculateEmi(
                newLoan.TermInMonths,
                newLoan.Amount,
                newLoan.InterestRate,
                newLoan.AppliedAt,
                newLoan
            );

  
            newLoan.EMISchedules = await _emi.GetEMISchedule(newLoan.LoanId);
            newLoan.EMIAmount = emi;
            newLoan.TotalPayable = totalPayable;
            newLoan.TotalPaid = 0;

            await _context.SaveChangesAsync();

            return newLoan;
        }

        public async Task<Loan?> UpdateAsync(int id , LoanDto loan)
        {
            var existing = await _context.Loans.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            existing.Status = loan.Status;
            existing.DocsVerified = loan.DocsVerified;
            existing.AdminReviewStatus = loan.AdminReviewStatus;
            existing.AdminReviewReason = loan.AdminReviewReason;
            existing.AdminReviewedAt = loan.AdminReviewedAt;
            existing.ManagerReviewStatus = loan.ManagerReviewStatus;
            existing.ManagerReviewReason = loan.ManagerReviewReason;
            existing.ManagerReviewedAt = loan.ManagerReviewedAt;
            existing.Purpose = loan.Purpose;
            existing.AppliedAt = loan.AppliedAt;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.Amount = loan.Amount;
            existing.TermInMonths = loan.TermInMonths;
            existing.UserId = loan.UserId;
            existing.LoanType = loan.LoanType;

            await _context.SaveChangesAsync();

            return await _context.Loans
                .Include(l => l.EMISchedules)
                .FirstOrDefaultAsync(l => l.LoanId == id);

        }

        public async Task<Loan?> DeleteAsync(int Id)
        {
            var existing = await _context.Loans.FindAsync(Id);
            if(existing != null)
            {
                _context.Remove(existing);
            }
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Loan?> UpdateDecisionAsync(int id, string reviewerRole, string status, string? reason)
        {
            var existing = await _context.Loans.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            ApplyReview(existing, reviewerRole, status, reason);
            RefreshFinalDecision(existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await _context.Loans
                .Include(l => l.EMISchedules)
                .FirstOrDefaultAsync(l => l.LoanId == id);
        }

        private static void ApplyReview(Loan loan, string reviewerRole, string status, string? reason)
        {
            var normalizedRole = reviewerRole.Trim();
            var normalizedStatus = NormalizeDecision(status);
            var normalizedReason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim();
            var reviewedAt = DateTime.UtcNow;

            if (normalizedRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                loan.AdminReviewStatus = normalizedStatus;
                loan.AdminReviewReason = normalizedReason;
                loan.AdminReviewedAt = reviewedAt;
                return;
            }

            if (normalizedRole.Equals("Manager", StringComparison.OrdinalIgnoreCase))
            {
                loan.ManagerReviewStatus = normalizedStatus;
                loan.ManagerReviewReason = normalizedReason;
                loan.ManagerReviewedAt = reviewedAt;
                return;
            }

            throw new InvalidOperationException("Only Admin or Manager can review a loan.");
        }

        private static void RefreshFinalDecision(Loan loan)
        {
            var adminDecision = NormalizeDecision(loan.AdminReviewStatus);
            var managerDecision = NormalizeDecision(loan.ManagerReviewStatus);

            if (adminDecision == "Rejected" || managerDecision == "Rejected")
            {
                loan.Status = "Rejected";
                return;
            }

            if (adminDecision == "Approved" && managerDecision == "Approved")
            {
                loan.Status = "Approved";
                return;
            }

            loan.Status = "Pending";
        }

        private static string NormalizeDecision(string status)
        {
            if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
            {
                return "Approved";
            }

            if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
            {
                return "Rejected";
            }

            return "Pending";
        }

        public async Task<Loan?> PayEmiAsync(int emiId)
        {
            var emi = await _context.EMISchedules
                .Include(e => e.Loan)
                .FirstOrDefaultAsync(e => e.Id == emiId);

            if (emi == null || emi.IsPaid)
                return null;

            emi.IsPaid = true;
            emi.PaidAt = DateTime.UtcNow;

            emi.Loan.TotalPaid += emi.EMIAmount;

            await _context.SaveChangesAsync();

            return await _context.Loans
                .Include(l => l.EMISchedules)
                .FirstOrDefaultAsync(l => l.LoanId == emi.LoanId);
        }
    }
}
