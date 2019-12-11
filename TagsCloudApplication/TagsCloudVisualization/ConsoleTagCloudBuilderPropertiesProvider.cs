using System.Drawing;

namespace TagsCloudVisualization
{
    public class ConsoleTagCloudBuilderPropertiesProvider
    {
        private readonly Options options;

        public ConsoleTagCloudBuilderPropertiesProvider(Options options)
        {
            this.options = options;
        }

        public CloudTagProperties GetCloudTagProperties()
        {
            return new CloudTagProperties(new FontFamily(options.FontFamilyName), options.FontSize);
        }

        public VisualizatorProperties GetVisualizatorProperties()
        {
            return new VisualizatorProperties(new Size(options.ImageSize[0], options.ImageSize[1]));
        }

        public ConstantTextColorProvider GetConstantTextColorProvider()
        {
            return new ConstantTextColorProvider(Color.FromName(options.FontColorName));
        }

        public ConsoleTagCloudBuilderIOSettings GetIOSettings()
        {
            return new ConsoleTagCloudBuilderIOSettings(options);
        }

        public Point GetCentralPoint()
        {
            return new Point(options.CentralPoint[0], options.CentralPoint[1]);
        }
    }
}
