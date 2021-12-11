using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class DrawerSettings
    {
        public Color TagColor { get; }

        public DrawerSettings(Color tagColor)
        {
            TagColor = tagColor;
        }
    }
}