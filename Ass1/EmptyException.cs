﻿using System.Runtime.Serialization;

namespace Ass1
{
    [Serializable]
    internal class EmptyException : Exception
    {
        public EmptyException()
        {
        }

        public EmptyException(string? message) : base(message)
        {
        }

        public EmptyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}