using LoanManagement.Data;
using LoanManagement.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LoanManagement.Services
{
    public class EMIService
    {
        private readonly AppDbContext _context;

        public EMIService(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // CALCULATE EMI
        // =========================
        public async Task<(double emi, double totalPayable)> CalculateEmi(
            int totalMonths,
            double amount,
            double interestRate,
            DateTime startDate,
            Loan loan)
        {
            Log.Information("Starting EMI calculation for LoanId: {LoanId}", loan.LoanId);

            if (totalMonths <= 0 || amount <= 0 || interestRate <= 0)
            {
                Log.Warning("Invalid EMI parameters: Months={Months}, Amount={Amount}, Rate={Rate}",
                    totalMonths, amount, interestRate);

                throw new ArgumentException("Invalid EMI calculation parameters");
            }

            double monthlyRate = interestRate / 12 / 100;

            double emi = amount * monthlyRate * Math.Pow(1 + monthlyRate, totalMonths)
                       / (Math.Pow(1 + monthlyRate, totalMonths) - 1);

            double totalPayable = emi * totalMonths;

            emi = Math.Round(emi, 2);

            var schedules = new List<EMISchedule>();

            double balance = amount;
            DateTime dueDate = startDate;

            for (int i = 1; i <= totalMonths; i++)
            {
                double interest = balance * monthlyRate;
                double principal = emi - interest;

                balance -= principal;
                dueDate = dueDate.AddMonths(1);

                schedules.Add(new EMISchedule
                {
                    LoanId = loan.LoanId,
                    MonthNumber = i,
                    DueDate = dueDate,
                    EMIAmount = emi,
                    InterestComponent = Math.Round(interest, 2),
                    PrincipalComponent = Math.Round(principal, 2),
                    RemainingBalance = Math.Round(balance, 2),
                    IsPaid = false,
                    PaidAt = null
                });
            }

            await _context.EMISchedules.AddRangeAsync(schedules);
            await _context.SaveChangesAsync();

            Log.Information("EMI schedule generated for LoanId: {LoanId} with {Count} installments",
                loan.LoanId, totalMonths);

            return (Math.Round(emi, 2), Math.Round(totalPayable, 2));
        }

        // =========================
        // GET EMI SCHEDULE
        // =========================
        public async Task<List<EMISchedule>> GetEMISchedule(int id)
        {
            Log.Information("Fetching EMI schedule for LoanId: {LoanId}", id);

            var result = await _context.EMISchedules
                .Where(e => e.LoanId == id)
                .OrderBy(e => e.MonthNumber)
                .ToListAsync();

            if (result == null || !result.Any())
            {
                Log.Warning("No EMI schedule found for LoanId: {LoanId}", id);
            }

            return result;
        }

        // =========================
        // PAY EMI
        // =========================
        public async Task<Loan?> PayEmi(int emiId)
        {
            Log.Information("Processing EMI payment for EMI ID: {EmiId}", emiId);

            var emi = await _context.EMISchedules
                .Include(e => e.Loan)
                .FirstOrDefaultAsync(e => e.Id == emiId);

            if (emi == null)
            {
                Log.Warning("Invalid EMI ID: {EmiId}", emiId);
                return null;
            }

            if (emi.IsPaid)
            {
                Log.Warning("EMI already paid for EMI ID: {EmiId}", emiId);
                return null;
            }

            // Mark EMI paid
            emi.IsPaid = true;
            emi.PaidAt = DateTime.UtcNow;

            // Update loan totals
            emi.Loan.TotalPaid += emi.EMIAmount;

            await _context.SaveChangesAsync();

            Log.Information("EMI payment successful for EMI ID: {EmiId}, LoanId: {LoanId}",
                emiId, emi.Loan.LoanId);

            return emi.Loan;
        }
    }
}