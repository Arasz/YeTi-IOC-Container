using System;

namespace YeTi.Exceptions
{
    public abstract class CompositionException : Exception
    {
        private Type Type { get; }

        public CompositionException(Type type)
        {
            Type = type;
        }
    }
}