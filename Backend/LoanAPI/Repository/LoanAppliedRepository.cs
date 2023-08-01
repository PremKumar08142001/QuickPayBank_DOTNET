using LoanAPI.Data;
using LoanAPI.Exceptions;
using LoanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanAPI.Repository
{
    public class LoanAppliedRepository : ILoanAppliedRepository
    {
        private readonly LoanAPIContext _dbcontext;

        public LoanAppliedRepository(LoanAPIContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
      
        public async Task<IEnumerable<LoanApplied>> GetAllLoansApplied()
        {
            return await _dbcontext.LoansApplied.ToListAsync();
        }
        public async Task<LoanApplied> GetLoanAppliedById(int id)
        {
            return await _dbcontext.LoansApplied.FindAsync(id);
        }
        public async Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByUserId(string userId)
        {
            return await _dbcontext.LoansApplied.Where(ln => ln.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByUserName(string userName)
        {
            return await _dbcontext.LoansApplied.Where(ln => ln.UserName.Equals(userName)).ToListAsync();
        }
        public async Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByLoanType(string loanType)
        {
            return await _dbcontext.LoansApplied.Where(ln => ln.LoanType == loanType).ToListAsync();
        }

        public async Task<bool> AddLoanApplied(LoanApplied loan)
        {
            bool isExist = _dbcontext.LoansApplied.Any(ln => ln.LoanType == loan.LoanType && ln.UserId == loan.UserId);
            if (isExist)
            {
                return false;
            }
            Console.WriteLine(loan.UserName+"........");
            _dbcontext.LoansApplied.Add(loan);
            await _dbcontext.SaveChangesAsync();
            return true;

        }
        public async Task<bool> UpdateLoanApplied(int appId, LoanApplied loan, string status)
        {
           
            var existLoan = _dbcontext.LoansApplied.Find(appId);
            if (existLoan != null)
            {
                existLoan.LoanStatus = status;
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UpdateAccountBalance(int appId, int amount)
        {
            var existLoan = _dbcontext.LoansApplied.Find(appId);
            if (existLoan == null)
                return false;
            Console.WriteLine(existLoan.AmountToPay + " +" + amount + "...........");
            existLoan.AmountToPay = existLoan.AmountToPay - amount;
            if (existLoan.AmountToPay < 0)
            {
                existLoan.AmountToPay = 0;
            }
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteLoanApplied(int id)
        {
            var loanTemp = await _dbcontext.LoansApplied.FindAsync(id);
            if (loanTemp != null)
            {
                _dbcontext.LoansApplied.Remove(_dbcontext.LoansApplied.Find(id));
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
