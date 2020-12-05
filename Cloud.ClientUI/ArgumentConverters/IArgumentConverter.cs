namespace Cloud.ClientUI.ArgumentConverters
{
    public interface IArgumentConverter
    {
        public ConvertedArguments ParseArguments(Arguments arguments);
    }
}