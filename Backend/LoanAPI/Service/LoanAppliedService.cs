using LoanAPI.Exceptions;
using LoanAPI.Models;
using LoanAPI.Repository;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace LoanAPI.Service
{
    public class LoanAppliedService : ILoanAppliedService
    {
        private readonly ILoanAppliedRepository _repository;

        public LoanAppliedService(ILoanAppliedRepository repository )
        {
            _repository = repository;
        }
        public async Task<bool> AddLoanApplied(LoanApplied loan)
        {
            try
            {
                bool result = await _repository.AddLoanApplied(loan);
                if (result)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex) { throw ex.InnerException; }
        }

        public async Task<bool> DeleteLoanApplied(int id)
        {
            try
            {
                return await _repository.DeleteLoanApplied(id);
            }
            catch
            {
                throw new LoanAppliedNotFoundException();
            }
        }

        public async Task<IEnumerable<LoanApplied>> GetAllLoansApplied()
        {
            try
            {
                return await _repository.GetAllLoansApplied();
            }
            catch
            {
                throw new LoanAppliedNotFoundException();
            }
        }

        public async Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByLoanType(string loanType)
        {
            try
            {
                return await _repository.GetAllLoansAppliedByLoanType(loanType);
            }
            catch
            {
                throw new LoanAppliedNotFoundException(loanType);
            }
        }

        public async Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByUserId(string userId)
        {
            try
            {
                return await _repository.GetAllLoansAppliedByUserId(userId);
            }
            catch
            {
                throw new LoanAppliedNotFoundException();
            }
        }

        public async Task<IEnumerable<LoanApplied>> GetAllLoansAppliedByUserName(string userName)
        {
            try
            {
                var result = await _repository.GetAllLoansAppliedByUserName(userName);
                if (result == null)
                {
                    throw new LoanAppliedNotFoundException();
                }
                else
                {
                    return result;
                }
            }
            catch(Exception ex) { throw ex.InnerException; }
        }

        public async Task<LoanApplied> GetLoanAppliedById(int id)
        {
            try
            {
                return await _repository.GetLoanAppliedById(id);
            }
            catch
            {
                throw new LoanAppliedNotFoundException(id.ToString());
            }
        }

        public async Task<bool> UpdateLoanApplied(int appId, LoanApplied loan, string status)
        {
            try
            {
                return await _repository.UpdateLoanApplied(appId, loan, status);
            }
            catch
            {
                throw new LoanAppliedNotFoundException();
            }
        }
        public async Task<bool> UpdateAccountBalance(int accountnumber, int amount)
        {
            Console.WriteLine(" +" + amount + "...........");
            if (await _repository.UpdateAccountBalance(accountnumber, amount))
            {
                return true;
            }
            throw new LoanAppliedNotFoundException();
        }
    }
}
