using System;
using System.Drawing;
using TagsCloudVisualization.Configs;

namespace CloudContainer.ConfigCreators
{
    public class ConsoleConfigCreator : IConfigCreator
    {
        public void CreateConfig(IConfig config, Arguments arguments)
        {
            if (arguments.Center == null || arguments.Color == null || arguments.Font == null ||
                arguments.ImageSize == null)
            {
                DefaultConfig.SetDefault(config);
                return;
            }

            var splittedCenter = SplitValue(arguments.Center);
            var splittedFont = SplitValue(arguments.Font);
            var splittedSize = SplitValue(arguments.ImageSize);

            var center = new Point(Convert.ToInt32(splittedCenter[0]), Convert.ToInt32(splittedCenter[1]));
            var color = Color.FromName(arguments.Color);
            var size = new Size(Convert.ToInt32(splittedSize[0]), Convert.ToInt32(splittedSize[1]));
            var font = new Font(splittedFont[0], Convert.ToInt32(splittedFont[1]));

            config.SetValues(font, center, color, size);
        }

        private static string[] SplitValue(string value)
        {
            return value.Split(';');
        }
    }
}