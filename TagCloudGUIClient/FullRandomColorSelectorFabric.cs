using TagCloudCreator;

namespace TagCloudGUIClient
{
    public class FullRandomColorSelectorFabric : IColorSelectorFabric
    {
        public string Name => "Случайный цвет";

        public IColorSelector Create()
        {
            return new FullRandomColorSelector();
        }
    }
}