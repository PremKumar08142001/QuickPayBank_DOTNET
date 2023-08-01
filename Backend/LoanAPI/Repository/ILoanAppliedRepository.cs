using LoanAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Repository
{
    public interface ILoanAppliedRepository
    {
        public Task<IEnumerable<LoanApplied>> GetAllLoansApplied();
        public Task<LoanApplied> GetLoanAppliedById(int id);
        public Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByUserId(string userId);
        public Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByUserName(string userName);
        public Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByLoanType(string loanType);
        public Task<bool> AddLoanApplied(LoanApplied loan);
        public Task<bool>UpdateLoanApplied(int appId, LoanApplied loan, string status);
        public Task<bool> DeleteLoanApplied(int id);
        public  Task<bool> UpdateAccountBalance(int appId, int amount);

    }
}
