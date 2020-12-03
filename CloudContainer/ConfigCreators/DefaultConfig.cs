using System.Drawing;
using TagsCloudVisualization.Configs;

namespace CloudContainer.ConfigCreators
{
    public static class DefaultConfig
    {
        public static void SetDefault(IConfig config)
        {
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                new Point(1500, 1500), Color.Blue, new Size(1500, 1500));
        }
    }
}