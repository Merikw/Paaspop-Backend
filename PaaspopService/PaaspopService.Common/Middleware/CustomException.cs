using System;
using System.Runtime.Serialization;

namespace PaaspopService.Common.Middleware
{
    [Serializable]
    public abstract class CustomException : Exception
    {
        protected CustomException()
        {
        }

        protected CustomException(string msg) : base(msg)
        {
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}