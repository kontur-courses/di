using System;

namespace TagsCloudContainer
{
    public class Result<T>
    {
        public bool Success { get; }

        public T Value
        {
            get
            {
                if (!Success)
                    throw new Exception("Can't get value of not successful result.");

                return value;
            }
        }

        public Exception Exception
        {
            get
            {
                if (Success)
                    throw new Exception("Can't get exception of successful result.");

                return exception;
            }
        }

        private readonly T value;
        private readonly Exception exception;

        public Result(T value)
        {
            Success = true;
            this.value = value;
        }

        public Result(Exception exception)
        {
            Success = false;
            this.exception = exception;
        }
    }
}