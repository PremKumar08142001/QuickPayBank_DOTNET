namespace LoanAPI.Exceptions
{
    public class LoanAppliedNotFoundException : ApplicationException
    {
        public LoanAppliedNotFoundException() { }
        public LoanAppliedNotFoundException(string message) : base(message) { }
    }
}
