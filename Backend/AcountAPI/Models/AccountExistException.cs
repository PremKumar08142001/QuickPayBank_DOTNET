using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace AcountAPI.Models
{
    public class AccountExistException:ApplicationException
    {
        public AccountExistException() { }
        public AccountExistException(String message) : base(message) { }
    }
}
