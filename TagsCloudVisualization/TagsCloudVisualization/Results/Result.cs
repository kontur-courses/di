using System;

namespace TagsCloudVisualization.Results
{
    public class None
    {
        private None()
        {
        }
    }

    public struct Result<T>
    {
        public Result(string error, T value = default)
        {
            Error = error;
            Value = value;
        }

        public static implicit operator Result<T>(T v)
        {
            return Result.Ok(v);
        }

        public string Error { get; }
        internal T Value { get; }

        public bool IsSuccess => Error == null;
    }

    public static class Result
    {
        public static Result<T> AsResult<T>(this T value)
        {
            return Ok(value);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(null, value);
        }

        public static Result<None> Ok()
        {
            return Ok<None>(null);
        }

        public static Result<T> Fail<T>(string e)
        {
            return new Result<T>(e);
        }

        public static Result<T> Of<T>(Func<T> f, string error = null)
        {
            try
            {
                return Ok(f());
            }
            catch (Exception e)
            {
                return Fail<T>(error ?? e.Message);
            }
        }

        public static Result<None> OfAction(Action f, string error = null)
        {
            try
            {
                f();
                return Ok();
            }
            catch (Exception e)
            {
                return Fail<None>(error ?? e.Message);
            }
        }
    }
}