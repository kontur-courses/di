using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class CloudParametersParser : ICloudParametersParser
    {
        public CloudParameters Parse(Options options, CloudParameters parameters)
        {
            parameters.Color = Color.FromName(options.Color);
            parameters.ImageSize = GetImageSize(options.ImageSize);
            parameters.FontName = options.FontName;

            return parameters;
        }

        private Size GetImageSize(string input)
        {
            var delimiter = input.IndexOf('x');
            int.TryParse(input.Substring(0, delimiter), out var width);
            int.TryParse(input.Substring(delimiter + 1), out var height);
            return new Size(width, height);
        }
    }
}