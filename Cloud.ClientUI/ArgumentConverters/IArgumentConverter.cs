using CloudContainer;

namespace Cloud.ClientUI.ArgumentConverters
{
    public interface IArgumentConverter
    {
        public TagCloudArguments ParseArguments(Arguments arguments);
    }
}