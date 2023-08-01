namespace LoanAPI.Exceptions
{
    public class LoanOfferedNotFoundException : ApplicationException
    {
        public LoanOfferedNotFoundException() { }
        public LoanOfferedNotFoundException(string message) : base(message) { }
        
    }
}
