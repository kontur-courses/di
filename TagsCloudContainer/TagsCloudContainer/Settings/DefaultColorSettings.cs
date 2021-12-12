using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public class DefaultColorSettings : IDefaultColorSettings
    {
        public Color Color { get; }

        public DefaultColorSettings(IRenderSettings settings)
        {
            Color = settings.DefaultColor;
        }
    }
}