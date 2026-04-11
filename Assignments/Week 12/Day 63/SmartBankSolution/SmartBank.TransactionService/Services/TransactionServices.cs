using SmartBank.TransactionService.DTOs;
using SmartBank.TransactionService.Models;
using SmartBank.TransactionService.Repositories;

namespace SmartBank.TransactionService.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionRepository _repo;

        public TransactionServices(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<TransactionResponseDTO> Create(TransactionCreateDTO dto)
        {
            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Type = dto.Type,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(transaction);

            return new TransactionResponseDTO
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Description = transaction.Description,
                CreatedAt = transaction.CreatedAt
            };
        }

        public async Task<List<TransactionResponseDTO>> GetByAccountId(int accountId)
        {
            var transactions = await _repo.GetByAccountId(accountId);

            return transactions.Select(t => new TransactionResponseDTO
            {
                Id = t.Id,
                AccountId = t.AccountId,
                Amount = t.Amount,
                Type = t.Type,
                Description = t.Description,
                CreatedAt = t.CreatedAt
            }).ToList();
        }
    }
}