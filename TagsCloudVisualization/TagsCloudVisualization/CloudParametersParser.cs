using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class CloudParametersParser : ICloudParametersParser
    {
        private readonly CloudParameters parameters;

        public CloudParametersParser(CloudParameters parameters)
        {
            this.parameters = parameters;
        }

        public CloudParameters Parse(Options options)
        {
            parameters.Color = Color.FromName(options.Color);
            parameters.ImageSize = GetImageSize(options.ImageSize);
            parameters.FontName = options.FontName;
            //double.TryParse(options.FactorStep, out var factorStep);
            //parameters.FactorStep = factorStep;
            //double.TryParse(options.DegreeStep, out var degreeStep);
            //parameters.FactorStep = degreeStep;

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