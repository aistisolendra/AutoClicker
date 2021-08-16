using System;

namespace AutoClicker.Exceptions
{
    [Serializable]
    public class UserControlException : Exception
    {
        public UserControlException() { }

        public UserControlException(string message) : base(message) { }

        public UserControlException(string message, Exception innerException) : base(
            message, innerException) { }
    }
}