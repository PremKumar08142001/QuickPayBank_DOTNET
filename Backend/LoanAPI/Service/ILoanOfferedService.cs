using LoanAPI.Models;

namespace LoanAPI.Service
{
    public interface ILoanOfferedService
    {
        public Task<IEnumerable<LoanOffered>> GetAllLoansOffered();
        public Task<LoanOffered> GetLoanById(int id);
        public Task<LoanOffered> GetLoanByLoanType(string loanType);
        public Task<bool> AddLoanOffered(LoanOffered newLoan);
        public Task<bool> UpdateLoanOffered(int id, LoanOffered loanOffered);
        public Task<bool> DeleteLoanOffered(int id);
    }
}
