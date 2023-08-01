using AcountAPI.Data;
using AcountAPI.Models;
using AcountAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace AcountAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        public AccountService(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public async Task<bool> CreateAccountReq(Account accountRequest)
        {
            if (await _accountRepo.CreateAccountReq(accountRequest))
            {
                return true;
            }
            throw new AccountExistException("Account for user already exist");
        }

        public async Task<bool> DeleteAccountReq(long accountRequestId)
        {

            if (await _accountRepo.DeleteAccountReq(accountRequestId))
            {
                return true;
            }
            throw new RequestAccNotFoundException("Account Request does not exixt");
        }

        public async Task<List<Account>> GetAllAccountReqsByBranchId(string branchId)
        {
            var allAccountReqs = await _accountRepo.GetAllAccountReqsByBranchId(branchId);
            if (allAccountReqs.Count() > 0) return allAccountReqs;

            throw new BranchNotFoundException("No branch found with this id");

        }
        public async Task<Account> GetByAccountReqId(long requestId)
        {
            var request = await _accountRepo.GetByAccountReqId(requestId);
            if (request == null)
            {
                throw new RequestAccNotFoundException("Account Request does not exixt");
            }

            return request;
        }
        public async Task<List<Account>> GetAllAccountReques()
        {
            var requests = await _accountRepo.GetAllAccountRequest();
            if (requests == null)
            {
                throw new RequestAccNotFoundException("Account Request does not exixt");
            }
            return requests;

        }
        public async Task<bool> UpdateAccountDetails(long accountnumber, string userName){
         if (await _accountRepo.UpdateAccountDetails(accountnumber, userName))
            {
                return true;
            }
            throw new RequestAccNotFoundException("Account Number does not exixt");

    }
   public async Task<bool> UpdateAccountBalance(long accountnumber, int amount) {
            Console.WriteLine( " +" + amount + "...........");
            if (await _accountRepo.UpdateAccountBalance(accountnumber, amount))
            {
                return true;
            }
            throw new RequestAccNotFoundException("Account Number does not exixt");
        }

        public async Task<bool> AccountExist(string username)
        {
            if (await _accountRepo.AccountExist(username)==true)
            {
                return true;
            }
            return false;
        }
        
        public async Task<Account> ByName(string username)
        {
            var account=await _accountRepo.ByName(username);
            if (account == null)
            {
                throw new RequestAccNotFoundException("user not exst exist");
            }
            return account;
            
        }

        public async Task<Account> ById(int userId)
        {
            var account = await _accountRepo.ById(userId);
            if (account == null)
            {
                throw new RequestAccNotFoundException("user not exst exist");
            }
            return account;

        }
        
         public async Task<List<Account>> ByBranchCode(string branchCode)
        {
            var account = await _accountRepo.ByBranchCode(branchCode);
            if (account.Count()==0)
            {
                throw new RequestAccNotFoundException("Branch not exist");
            }
            return account;

        }
        }
    }
