using SmartBank.TransactionService.DTOs;

namespace SmartBank.TransactionService.Services
{
    public interface ITransactionServices
    {
        Task<TransactionResponseDTO> Create(TransactionCreateDTO dto);

        Task<List<TransactionResponseDTO>> GetByAccountId(int accountId);
    }
}