using System;

namespace YeTi.Exceptions
{
    public class ComponentHasMultipleConstructorsException : CompositionException
    {
        public ComponentHasMultipleConstructorsException(Type type) : base(type)
        {
        }
    }
}