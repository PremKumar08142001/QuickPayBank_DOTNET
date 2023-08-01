using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationAPI.Exceptions
{
    public class UserAlreadyExistException : ApplicationException
    {
        public UserAlreadyExistException() { }
        public UserAlreadyExistException(string message) : base(message) { }
    }
}
