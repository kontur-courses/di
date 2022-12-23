using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public static class ResultExtensions
    {
        public static Result OnFail(this Result input, Action<Result> handleAction)
        {
            if (input.IsFailed)
                handleAction(input);
            return input;
        }
        public static Result<TInput> OnFail<TInput>(this Result<TInput> input, Action<Result<TInput>> handleAction)
        {
            if (input.IsFailed)
                handleAction(input);
            return input;
        }

        public static Result OnSuccess(this Result input, Action<Result> handleAction)
        {
            if (input.IsSuccess)
                handleAction(input);
            return input;
        }
        public static Result<TInput> OnSuccess<TInput>(this Result<TInput> input, Action<Result<TInput>> handleAction)
        {
            if (input.IsSuccess)
                handleAction(input);
            return input;
        }
    }
}