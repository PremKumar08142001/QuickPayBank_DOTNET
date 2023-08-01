using AcountAPI.Data;
using AcountAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace AcountAPI.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly AccountDBContext _dbContext;
        public AccountRepository(AccountDBContext dbContext) {
        _dbContext = dbContext;
        }
        public async Task<bool> CreateAccountReq(Account accountRequest)
        {
            bool exist= _dbContext.Accounts.Any(a=>a.UserName.Equals(accountRequest.UserName));
            if (exist)
                return false;
            Account acc = new Account()
            {
                BranchName = accountRequest.BranchName,
                BranchCode = accountRequest.BranchCode,
                UserName = accountRequest.UserName,
                UserId= accountRequest.UserId,
            };
            Console.WriteLine(".........start");
            var account = _dbContext.Accounts.Add(acc);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAccountReq(long accountRequestId)
        {
           
            if (_dbContext.Accounts == null)
                return false;

            var account = await _dbContext.Accounts.FindAsync(accountRequestId);
            if (account == null)
                return false;
           _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Account>> GetAllAccountReqsByBranchId(string branchId)
        {
            var all =await _dbContext.Accounts.Where(a=>a.BranchCode.Equals(branchId)).ToListAsync();
            
            return all;

        }
        public async Task<Account> GetByAccountReqId(long requestId)
        {
            var account = await _dbContext.Accounts.FindAsync(requestId);

            return account;

        }
        public async Task<List<Account>> GetAllAccountRequest()
        {
            var accounts = await _dbContext.Accounts.ToListAsync();

            return accounts;

        }

        public async Task<bool> UpdateAccountDetails(long accountnumber,string userName)
        {
            var account= await _dbContext.Accounts.FindAsync(accountnumber);
            if (account==null)
                return false;
           
            account.UserName = userName;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAccountBalance(long accountnumber, int amount)
        {
            var account = await _dbContext.Accounts.FindAsync(accountnumber);
            if (account == null)
                return false;
            Console.WriteLine(account.Balance +" +"+ amount+ "...........");
            account.Balance = account.Balance-amount;
            Console.WriteLine(account.Balance+"...........");
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AccountExist(string username)
        {
            bool exist = _dbContext.Accounts.Any(a => a.UserName.Equals(username));
         

            if (exist)
                return true;
            Console.WriteLine($"{exist}......................");
            return false;
        }
        public async Task<Account> ByName(string username)
        {
            var account = await _dbContext.Accounts.Where(a=>a.UserName.Equals(username)).FirstOrDefaultAsync();


          
            return account;
        }
        public async Task<Account> ById(int userId)
        {
            var account = await _dbContext.Accounts.Where(a => a.UserId== userId).FirstOrDefaultAsync();

            return account;
        }
        public async Task<List<Account>> ByBranchCode(string branchCode)
        {
            var account = await _dbContext.Accounts.Where(a => a.BranchCode.Equals(branchCode)).ToListAsync();

            return account;
        }


    }
}
