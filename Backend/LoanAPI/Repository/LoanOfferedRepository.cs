using LoanAPI.Data;
using LoanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanAPI.Repository
{
    public class LoanOfferedRepository : ILoanOfferedRepository
    {
        private readonly LoanAPIContext _dbcontext;

        public LoanOfferedRepository(LoanAPIContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<bool> AddLoanOffered(LoanOffered newLoan)
        {
            bool isExist = _dbcontext.LoansOffered.Any(ln => ln.LoanType == newLoan.LoanType);
            if (isExist)
            {
                return false;
            }
            _dbcontext.LoansOffered.Add(newLoan);
            await _dbcontext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteLoanOffered(int id)
        {
            var loanTemp = await _dbcontext.LoansOffered.FindAsync(id);
            if (loanTemp != null)
            {
                _dbcontext.LoansOffered.Remove(_dbcontext.LoansOffered.Find(id));
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<LoanOffered>> GetAllLoansOffered()
        {
            return await _dbcontext.LoansOffered.ToListAsync();
        }

        public async Task<LoanOffered> GetLoanById(int id)
        {
            return await _dbcontext.LoansOffered.FindAsync(id);
        }

        public async Task<LoanOffered> GetLoanByLoanType(string loanType)
        {
            return await _dbcontext.LoansOffered.Where(ln => ln.LoanType == loanType).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateLoanOffered(int id, LoanOffered loanOffered)
        {
            var existLoan = _dbcontext.LoansOffered.Find(id);
            if (existLoan != null)
            {
                existLoan.LoanType = loanOffered.LoanType;
                existLoan.Interest = loanOffered.Interest;
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
