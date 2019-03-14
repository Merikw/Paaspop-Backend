using System;

namespace PaaspopService.Common.Middleware
{
    public abstract class CustomException : Exception
    {
        protected CustomException(string msg) : base(msg)
        {
        }
    }
}
