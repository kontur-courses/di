using System.Drawing.Imaging;
using TagCloudPainter.Interfaces;

namespace TagCloudPainter.Savers;

public class PngTagCloudSaver : ITagCloudSaver
{
    public readonly ITagCloudElementsBuilder _builder;
    public readonly ICloudPainter _painter;

    public PngTagCloudSaver(ICloudPainter painter, ITagCloudElementsBuilder builder)
    {
        _painter = painter;
        _builder = builder;
    }

    public void SaveTagCloud(string outputPath, string inputPath)
    {
        var words = _builder.GetTags(inputPath);
        var btm = _painter.PaintTagCloud(words);
        btm.Save(outputPath, ImageFormat.Png);
    }
}