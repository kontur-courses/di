using TagsCloudContainer.Core;

namespace TagsCloudContainer.UserInterface
{
    public interface IParametersProvider
    {
        bool TryGetParameters(string[] programArgs, out Parameters parameters);
    }
}