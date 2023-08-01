using BranchAPI.Models;
using BranchAPI.Repository;

namespace BranchAPI.Services
{
    public class BranchService:IBranchService
    {
        private readonly IBranchRepository _branchRepo;
        public BranchService(IBranchRepository branchRepo)
        {
            _branchRepo = branchRepo;
        }
        public async Task<bool> CreateBranch(Branch branch)
        {
            if (await _branchRepo.CreateBranch(branch))
            {
                Console.WriteLine();
                return true;
            }
            throw new BranchExistException("Branch already exist");
        }
        public async Task<bool> DeleteBranch(int branchId)
        {
            if (await _branchRepo.DeleteBranch(branchId))
            {
                return true;
            }
            throw new BranchNotFoundException("Branch does not exixt");
        }
        public async Task<Branch> GetByBranchId(String username)
        {
            var bank = await _branchRepo.GetByBranchId(username);
            if (bank == null)
            {
                throw new BranchNotFoundException("Branch does not exixt");
            }

            return bank;
        }
        public async Task<List<Branch>> GetAllBranch()
        {
            var branch = await _branchRepo.GetAllBranch();
            if (branch == null)
            {
                throw new BranchNotFoundException("Branch does not exixt");
            }
            return branch;
        }
    /*    public async Task<List<Branch>> GetAllByUserId(int userId)
        {
            var branches = await _branchRepo.GetAllByUserId(userId);
            if (branches == null)
            {
                throw new BranchNotFoundException("Branch does not exixt");
            }

            return branches;
        }*/
        public async Task<bool> UpdateBranch(int branchId, Branch branch)
        {
            if (await _branchRepo.UpdateBranch(branchId, branch))
            {
                return true;
            }
            throw new BranchNotFoundException("Branch does not exixt");
        }
        public async Task<List<String>> GetAllBranches()
        {
            return await _branchRepo.GetAllBranches();
        }
    }
}
