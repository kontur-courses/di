using System.Drawing;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class DefaultColorSettings : IDefaultColorSettings
    {
        public Color Color { get; }

        public DefaultColorSettings(IRenderOptions renderOptions, IArgbColorParser parser)
        {
            Color = parser.Parse(renderOptions.DefaultColor);
        }
    }
}