namespace BranchAPI.Models
{
    public class BranchExistException:ApplicationException
    {
        public BranchExistException() { }
        public BranchExistException(string message) : base(message) { }
    }
}
