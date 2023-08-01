using BranchAPI.Models;

namespace BranchAPI.Repository
{
    public interface IBranchRepository
    {
        Task<bool> CreateBranch(Branch branch);
        Task<bool> DeleteBranch(int branchId);
        Task<Branch> GetByBranchId(String username);
        Task<List<Branch>> GetAllBranch();
        //Task<List<Branch>> GetAllByUserId(int userId);
        Task<bool> UpdateBranch(int branchId, Branch branch);
        Task<List<String>> GetAllBranches();


    }
}
