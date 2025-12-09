using System;

namespace Exeptions
{
    [Serializable]
    public class HoleIsEmptyException : Exception
    {
        public HoleIsEmptyException()
        { }

        public HoleIsEmptyException(string message)
            : base(message)
        { }

        public HoleIsEmptyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
    
    [Serializable]
    public class HoleIsFullException : Exception
    {
        public HoleIsFullException()
        { }

        public HoleIsFullException(string message)
            : base(message)
        { }

        public HoleIsFullException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}