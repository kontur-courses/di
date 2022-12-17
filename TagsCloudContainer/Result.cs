namespace TagsCloudContainer;

public class Result<T>
{
    public readonly T Value = default;
    public readonly Exception Exception = null;
    public readonly bool Successful;
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