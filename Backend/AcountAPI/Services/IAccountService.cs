using AcountAPI.Models;

namespace AcountAPI.Services
{
    public interface IAccountService
    {
        Task<bool> CreateAccountReq(Account accountRequest);
        Task<bool> DeleteAccountReq(long accountRequestId);
        Task<Account> GetByAccountReqId(long accountRequestId);
        Task<List<Account>> GetAllAccountReqsByBranchId(string branchId);
        Task<List<Account>> GetAllAccountReques();
        Task<bool> UpdateAccountDetails(long accountnumber, string userName);
        Task<bool> UpdateAccountBalance(long accountnumber, int amount);
        Task<bool> AccountExist(string username);
        Task<Account> ByName(string username);
        Task<Account> ById(int userId);
        Task<List<Account>> ByBranchCode(string branchCode);

    }
}
