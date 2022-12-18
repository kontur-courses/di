using System.Drawing;
using System.Drawing.Imaging;
using TagCloudPainter.Builders;
using TagCloudPainter.FileReader;
using TagCloudPainter.Painters;
using TagCloudPainter.Preprocessors;

namespace TagCloudPainter.Savers;

public class TagCloudSaver : ITagCloudSaver
{
    private readonly IFileReader _reader;
    private readonly IWordPreprocessor _wordPreprocessor;
    private readonly ITagCloudElementsBuilder _builder;
    private readonly ICloudPainter _painter;

    public TagCloudSaver(IFileReader reader, IWordPreprocessor wordPreprocessor, ITagCloudElementsBuilder builder, ICloudPainter painter)
    {
        _reader = reader;
        _wordPreprocessor = wordPreprocessor;
        _builder = builder;
        _painter = painter;
    }

    public void SaveTagCloud(string inputPath,string outputPath, ImageFormat format)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException();
        var words = _reader.ReadFile(inputPath);
        var dictionary = _wordPreprocessor.GetWordsCountDictionary(words);
        var tags = _builder.GetTags(dictionary);
        var btm = _painter.PaintTagCloud(tags);
        btm.Save(outputPath, format);
    }
}