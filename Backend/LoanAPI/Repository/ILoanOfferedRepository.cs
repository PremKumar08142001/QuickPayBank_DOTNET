using LoanAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Repository
{
    public interface ILoanOfferedRepository
    {
        public Task<IEnumerable<LoanOffered>> GetAllLoansOffered();
        public Task<LoanOffered> GetLoanById(int id);
        public Task<LoanOffered> GetLoanByLoanType(string loanType);
        public Task<bool> AddLoanOffered(LoanOffered newLoan);
        public Task<bool> UpdateLoanOffered(int id, LoanOffered loanOffered);
        public Task<bool> DeleteLoanOffered(int id);
    }
}
