namespace TagsCloudContainer;

public class Result<T>
{
    public readonly Exception Exception;
    public readonly bool Successful;
    public readonly T Value;

    public Result(T value)
    {
        Value = value;
        Successful = true;
    }

    public Result(Exception exception)
    {
        Exception = exception;
        Successful = false;
    }
}

public static class Result
{
    public static Result<T> Execute<T>(this Result<T> result, Action action)
    {
        if (!result.Successful) return result;

        try
        {
            action();
            return result;
        }
        catch (Exception e)
        {
            return new Result<T>(e);
        }
    }

    public static Result<K> Convert<T, K>(this Result<T> result, Func<K> action)
    {
        if (!result.Successful) return new Result<K>(result.Exception);

        try
        {
            return new Result<K>(action());
        }
        catch (Exception e)
        {
            return new Result<K>(e);
        }
    }

    public static Result<T> GetResult<T>(Func<T> function)
    {
        try
        {
            var a = function();
            return new Result<T>(a);
        }
        catch (Exception e)
        {
            return new Result<T>(e);
        }
    }
}