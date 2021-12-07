using System;

namespace TagsCloudContainer
{
    public class Result<T>
    {
        public bool Success { get; }
        public T Value { get; }
        public Exception Exception { get; }

        public Result(T value)
        {
            Success = true;
            Value = value;
        }

        public Result(Exception exception)
        {
            Success = false;
            Exception = exception;
        }
    }
}