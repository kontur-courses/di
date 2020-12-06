using TagCloudCreator;

namespace TagCloudGUIClient
{
    public interface IColorSelectorFabric
    {
        string Name { get; }
        IColorSelector Create();
    }
}