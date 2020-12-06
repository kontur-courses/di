using System.Drawing;
using TagCloudCreator;

namespace TagCloudGUIClient
{
    public class BlackColorSelectorFabric : IColorSelectorFabric
    {
        public string Name => "Черный";

        public IColorSelector Create()
        {
            return new SingleColorSelector(Color.Black);
        }
    }
}