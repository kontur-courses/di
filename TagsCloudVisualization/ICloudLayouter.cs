namespace TagsCloudVisualization;

public interface ICloudLayouter
{
    IEnumerable<TextRectangle> CreateLayout();
}