using System.Drawing;

namespace TagsCloudVisualization;

public interface ICloudGenerator
{
    CircularCloudLayouter Layouter { get; set; }
    IPreprocessor Preprocessor { get; set; }
    Dictionary<string, Rectangle> GenerateCloud(string text);
}