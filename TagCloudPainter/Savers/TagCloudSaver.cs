using System.Drawing.Imaging;
using TagCloudPainter.Builders;
using TagCloudPainter.FileReader;
using TagCloudPainter.Painters;
using TagCloudPainter.Preprocessors;

namespace TagCloudPainter.Savers;

public class TagCloudSaver : ITagCloudSaver
{
    public readonly ITagCloudElementsBuilder _builder;
    public readonly ICloudPainter _painter;
    public readonly IFileReader _reader;
    public readonly IWordPreprocessor _wordPreprocessor;

    public TagCloudSaver(ICloudPainter painter, ITagCloudElementsBuilder builder, IFileReader reader,
        IWordPreprocessor wordPreprocessor)
    {
        _painter = painter;
        _builder = builder;
        _reader = reader;
        _wordPreprocessor = wordPreprocessor;
    }

    public ImageFormat Format { get; set; }

    public void SaveTagCloud(string outputPath, string inputPath)
    {
        var words = _reader.ReadFile(inputPath);
        var dictionary = _wordPreprocessor.GetWordsCountDictionary(words);
        var tags = _builder.GetTags(dictionary);
        var btm = _painter.PaintTagCloud(tags);
        btm.Save(outputPath, Format);
    }
}