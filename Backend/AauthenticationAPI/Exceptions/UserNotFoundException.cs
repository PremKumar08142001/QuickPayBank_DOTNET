﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationAPI.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string message) : base(message) { }
    }
}
