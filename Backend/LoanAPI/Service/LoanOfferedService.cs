using LoanAPI.Exceptions;
using LoanAPI.Models;
using LoanAPI.Repository;

namespace LoanAPI.Service
{
    public class LoanOfferedService : ILoanOfferedService
    {
        private readonly ILoanOfferedRepository _repository;

        public LoanOfferedService(ILoanOfferedRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddLoanOffered(LoanOffered newLoan)
        {
            var existLoan = await _repository.GetLoanByLoanType(newLoan.LoanType);
            if (existLoan != null)
            {
                return false;
            }
            else
            {
                await _repository.AddLoanOffered(newLoan);
                return true;
            }
        }

        public async Task<bool> DeleteLoanOffered(int id)
        {
            var isExist = await _repository.GetLoanById(id); 
            if (isExist != null)
            {
                await _repository.DeleteLoanOffered(id);
                return true;
            }
            else { throw new LoanOfferedNotFoundException(); }
        }

        public async Task<IEnumerable<LoanOffered>> GetAllLoansOffered()
        {
            try
            {
                return await _repository.GetAllLoansOffered();
            }
            catch
            {
                throw new LoanOfferedNotFoundException();
            }
        }

        public async Task<LoanOffered> GetLoanById(int id)
        {
            try
            {
                return await _repository.GetLoanById(id);
            }
            catch
            {
                throw new LoanOfferedNotFoundException();
            }
        }

        public async Task<LoanOffered> GetLoanByLoanType(string loanType)
        {
            try
            {
                return await _repository.GetLoanByLoanType(loanType);
            }
            catch
            {
                throw new LoanOfferedNotFoundException(loanType);
            }
        }

        public async Task<bool> UpdateLoanOffered(int id, LoanOffered loanOffered)
        {
            try
            {
                return await _repository.UpdateLoanOffered(id, loanOffered);
            }
            catch { throw new LoanOfferedNotFoundException(); };
        }
    }
}
