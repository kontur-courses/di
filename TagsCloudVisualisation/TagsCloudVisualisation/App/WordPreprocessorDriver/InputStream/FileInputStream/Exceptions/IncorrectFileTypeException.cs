using System;

namespace TagsCloudVisualisation.App.InputStream.FileInputStream.Exceptions
{
    public class IncorrectFileTypeException : Exception
    {
        public readonly string ExpectedType;
        public readonly string ActualType;
        
        public IncorrectFileTypeException(string expectedType, string actualType, string message)
            : base(message)
        {
            ExpectedType = expectedType;
            ActualType = actualType;
        }
        
        public IncorrectFileTypeException(string expectedType, string actualType)
        {
            ExpectedType = expectedType;
            ActualType = actualType;
        }
        
        public IncorrectFileTypeException(string expectedType, string actualType, string message, Exception e)
            : base(message, e)
        {
            ExpectedType = expectedType;
            ActualType = actualType;
        }
    }
}