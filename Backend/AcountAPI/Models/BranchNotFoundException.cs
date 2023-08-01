namespace AcountAPI.Models
{
    public class BranchNotFoundException:ApplicationException
    {
        public BranchNotFoundException() { }
        public BranchNotFoundException(string message) : base(message) { }
    }
}
