using SmartBank.AccountService.Data;
using SmartBank.AccountService.DTOs;
using SmartBank.AccountService.Exceptions;
using SmartBank.AccountService.Models;
using SmartBank.AccountService.Repositories;

namespace SmartBank.AccountService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        private readonly AccountDbContext _context;
        private readonly ITransactionApiClient _transactionClient;

        public AccountService(IAccountRepository repo, AccountDbContext context,
            ITransactionApiClient transactionClient)
        {
            _repo = repo;
            _context = context;
            _transactionClient = transactionClient;
        }

        public async Task<Account> CreateAccount(string userId)
        {
            var account = new Account
            {
                UserId = userId,
                AccountNumber = Guid.NewGuid().ToString().Replace("-","").Substring(0, 10),
                Balance = 0,
                CreatedAt = DateTime.Now
            };

            await _repo.AddAsync(account);
            return account;
        }

        public async Task<Account> GetAccount(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Account>> GetAllAccounts(string userid)
        {
            return await _repo.GetAllAccountsAsync(userid);
        }

        //public async Task Deposit(int accountId, decimal amount)
        //{
        //    var account = await _repo.GetByIdAsync(accountId);

        //    account.Balance += amount;

        //    _context.Transactions.Add(new Transaction
        //    {
        //        AccountId = accountId,
        //        Amount = amount,
        //        Type = "Deposit",
        //        Date = DateTime.Now
        //    });

        //    await _repo.UpdateAsync(account);
        //}

        // for using transaction microservice by passing JWT
        public async Task Deposit(int accountId, decimal amount, string token)
        {
            var account = await _repo.GetByIdAsync(accountId);

            account.Balance += amount;

            await _repo.UpdateAsync(account);

            // Call Transaction Service
            await _transactionClient.CreateTransaction(new TransactionCreateDto
            {
                AccountId = accountId,
                Amount = amount,
                Type = "Deposit",
                Description = "Deposit via AccountService"
            }, token);
        }

        //public async Task Withdraw(int accountId, decimal amount)
        //{
        //    var account = await _repo.GetByIdAsync(accountId);

        //    if (account.Balance < amount)
        //        throw new Exception("Insufficient balance");

        //    account.Balance -= amount;

        //    _context.Transactions.Add(new Transaction
        //    {
        //        AccountId = accountId,
        //        Amount = amount,
        //        Type = "Withdraw",
        //        Date = DateTime.Now
        //    });

        //    await _repo.UpdateAsync(account);
        //}

        // for using transaction microservice by passing JWT
        public async Task Withdraw(int accountId, decimal amount, string token)
        {
            var account = await _repo.GetByIdAsync(accountId);

            if (account.Balance < amount)
                throw new BadRequestException("Insufficient balance");
                // throw new Exception("Insufficient balance");

            account.Balance -= amount;

            await _repo.UpdateAsync(account);

            await _transactionClient.CreateTransaction(new TransactionCreateDto
            {
                AccountId = accountId,
                Amount = amount,
                Type = "Withdraw",
                Description = "Withdraw via AccountService"
            }, token);
        }
    }
}
