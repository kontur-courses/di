using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ResultProject
{
    public readonly struct Result<T>
    {
        public string? Error { get; }
        internal T? Value { get; }
        public bool IsSuccess => Error == null;
        
        public Result(string? error, T? value = default)
        {
            Error = error;
            Value = value;
        }
        
        public static implicit operator Result<T>(T v)
        {
            return Result.Ok(v);
        }

        public T? GetValueOrThrow()
        {
            if (IsSuccess) return Value;
            throw new InvalidOperationException($"No value. Only Error {Error}");
        }
    }

    public static class Result
    {
        public static Result<T> AsResult<T>(this T value) => Ok(value);

        public static Result<T> Ok<T>(T? value) => new(null, value);

        public static Result<None> Ok() => Ok<None>(null);

        public static Result<T> Fail<T>(string e) => new(e);

        public static Result<T> Of<T>(Action f, T value, string? error = default)
        {
            try
            {
                f();
                return Ok(value);
            }
            catch (Exception e)
            {
                return Fail<T>(error ?? e.Message);
            }
        }

        public static Result<T> Of<T>(Func<T> f, string? error = default)
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

        public static Result<None> OfAction(Action f, string? error = default)
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
        
        public static Result<TInput> ThenAction<TInput>(this Result<TInput> input, Action<TInput> continuation)
        {
            if (!input.IsSuccess) return Fail<TInput>(input.Error);
            return Of(() => continuation(input.Value), input.Value);
            // return input;
        }

        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input, Func<TInput, TOutput> continuation) 
            => input.Then(inp => Of(() => continuation(inp)));

        public static Result<bool> ThenCheckAllForFail<TInput, TOutput>(this Result<TInput> input, Func<TInput, IEnumerable<Result<TOutput>>> continuations)
        {
            if (!input.IsSuccess) return Fail<bool>(input.Error);
            foreach (var r in continuations(input.Value))
            {
                if (!r.IsSuccess) return Fail<bool>(r.Error);
            }
            return Ok(false);
        }
        
        public static Result<bool> ThenCombineAndCheckAllForFail<TInput, TOutput>(this Result<TInput> input, params Func<TInput, Result<TOutput>>[] continuations)
        {
            if (!input.IsSuccess) return Fail<bool>(input.Error);
            foreach (var continuation in continuations)
            {
                var r = continuation(input.Value);
                if (!r.IsSuccess) return Fail<bool>(r.Error);
            }
            return Ok(false);
        }

        public static Result<None> Then<TInput>(this Result<TInput> input, Action<TInput> continuation) 
            => input.Then(inp => OfAction(() => continuation(inp)));
        
        public static Result<TCast> ThenCast<TInput, TCast>(this Result<TInput> input)
        {
            if (!input.IsSuccess) return Fail<TCast>(input.Error);
            return new Result<TCast>(input.Error, (TCast) Convert.ChangeType(input.Value, typeof(TCast)));
        }

        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input, Func<TInput, Result<TOutput>> continuation) 
            => input.IsSuccess ? continuation(input.Value) : Fail<TOutput>(input.Error);
        
        public static Result<T> ValidateNull<T>(this Result<T> input, string message)
        {
            if (!input.IsSuccess) return input;
            return input.Value ?? Fail<T>(message);
        }

        public static Result<IEnumerable<TOutput>> ThenForEach<TInput, TOutput>(this Result<IEnumerable<TInput>> input, Func<TInput, TOutput> continuation)
        {
            if (!input.IsSuccess) return Fail<IEnumerable<TOutput>>(input.Error);
            return input.Value.Select(value => continuation(value)).ToList();
        }
        
        public static Result<IEnumerable<TOutput>> ThenForEach<TInput1, TInput2, TOutput>(this Result<IDictionary<TInput1, TInput2>> input, Func<KeyValuePair<TInput1, TInput2>, TOutput> continuation)
        {
            if (!input.IsSuccess) return Fail<IEnumerable<TOutput>>(input.Error);
            return input.Value.Select(value => continuation(value)).ToList();
        }

        public static Result<TInput> OnFail<TInput>(this Result<TInput> input, Action<string> handleError)
        {
            if (!input.IsSuccess) handleError(input.Error);
            return input;
        }

        public static Result<TInput> ThenFailIf<TInput>(this Result<TInput> input, Func<TInput, bool> continuation, string message)
        {
            if (!input.IsSuccess) return input;
            return continuation(input.Value) ? Fail<TInput>(message) : input;
        }

        public static Result<TInput> ReplaceError<TInput>(this Result<TInput> input, Func<string, string> replaceError) 
            => input.IsSuccess ? input : Fail<TInput>(replaceError(input.Error));

        public static Result<TInput> RefineError<TInput>(this Result<TInput> input, string errorMessage) 
            => input.ReplaceError(err => errorMessage + ". " + err);
    }
}