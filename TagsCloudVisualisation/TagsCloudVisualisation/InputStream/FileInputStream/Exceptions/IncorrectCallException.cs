using System;

namespace TagsCloudVisualisation.InputStream.FileInputStream.Exceptions
{
    public class IncorrectCallException : Exception
    {
        public IncorrectCallException(string message)
            : base(message) { }

        public IncorrectCallException(string message, Exception e)
            : base(message, e) {}
    }
}