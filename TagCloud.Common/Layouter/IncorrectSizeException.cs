namespace TagCloud.Common.Layouter;

public class IncorrectSizeException : Exception
{
    public IncorrectSizeException() : base("Size of rectangle must be positive")
    {
    }
}