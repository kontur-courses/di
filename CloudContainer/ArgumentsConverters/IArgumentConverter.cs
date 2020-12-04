namespace CloudContainer.ArgumentsConverters
{
    public interface IArgumentConverter
    {
        public ConvertedArguments ParseArguments(Arguments arguments);
    }
}