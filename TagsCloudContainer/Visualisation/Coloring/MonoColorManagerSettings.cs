using System.Drawing;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Visualisation.Coloring
{
    public class MonoColorManagerSettings
    {
        public Color Color;

        public MonoColorManagerSettings(IUI ui)
        {
            Color = ui.ApplicationSettings.ImageSettings.TextColor;
        }
    }
}