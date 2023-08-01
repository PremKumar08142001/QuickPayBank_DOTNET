namespace AcountAPI.Models
{
    public class RequestAccNotFoundException:ApplicationException
    {
        public RequestAccNotFoundException() { }   
        public RequestAccNotFoundException(string message) : base(message) { }
    }
}
