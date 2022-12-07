using System.Drawing;

namespace TagsCloudVisualization;

public interface ICloudGenerator
{
    CircularCloudLayouter Layouter { get; set; }
    IPreprocessor Preprocessor { get; set; }
    Dictionary<string, Point> GenerateCloud(string text);
}