using BranchAPI.Data;
using BranchAPI.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace BranchAPI.Repository
{
    public class BranchRepository:IBranchRepository
    {
        private readonly BranchDBContext _dbContext;
        public BranchRepository(BranchDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateBranch(Branch branch)
        {
            bool exist = _dbContext.Branches.Any(a => a.BranchName == branch.BranchName);
            if (exist)
                return false;
            var newModel = new Branch()
            {
                
                // Set the properties of the new model
               // BranchAddress = branch.BranchAddress,
               UserName = branch.UserName,
                BranchName = branch.BranchName
            };

            Console.WriteLine($"{newModel.BranchId}  {newModel.BranchCode}..............");
            // Add the new model to the context
            _dbContext.Branches.Add(newModel);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBranch(int branchId)
        {
            if (_dbContext.Branches == null)
                return false;

            var branch = await _dbContext.Branches.FindAsync(branchId);
            if (branch == null)
                return false;
            _dbContext.Branches.Remove(branch);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Branch> GetByBranchId(String username)
        {
            var branche = await _dbContext.Branches.Where(b=>b.UserName.Equals(username)).FirstOrDefaultAsync();

            return branche;
        }
        public async Task<List<Branch>> GetAllBranch()
        {
            var allbranches = await _dbContext.Branches.ToListAsync();

            return allbranches;
        }
     /*   public async Task<List<Branch>> GetAllByUserId(string userId)
        {
            var allbranches = await _dbContext.Branches.Where(b=>b.UserName.Equals(userId)).ToListAsync();

            return allbranches;*/
        
        public async Task<bool> UpdateBranch(int branchId, Branch branch)
        {
            var barnchE = await _dbContext.Branches.FindAsync(branchId);
            if (barnchE == null)
                return false;

            barnchE.BranchName = branch.BranchName;
            //barnchE.BranchAddress = branch.BranchAddress;
            //barnchE.BranchCode=branch.BranchCode;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<String>> GetAllBranches()
        {
            var allbranches = await _dbContext.Branches.Select(a=>a.BranchCode).ToListAsync();  

            return allbranches;
        }
    }
}
