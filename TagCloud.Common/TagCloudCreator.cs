using System.Drawing;
using TagCloud.Common.Drawing;
using TagCloud.Common.Options;
using TagCloud.Common.TagsConverter;
using TagCloud.Common.TextFilter;

namespace TagCloud.Common;

public class TagCloudCreator
{
    private ICloudDrawer drawer;
    private ITagsConverter converter;
    private ITextFilter filter;

    public TagCloudCreator(ICloudDrawer drawer, ITagsConverter converter, ITextFilter filter)
    {
        this.drawer = drawer;
        this.converter = converter;
        this.filter = filter;
    }

    public Bitmap CreateCloud(WordsOptions wordsOptions)
    {
        var lines = File.ReadAllLines(wordsOptions.PathToTextFile);
        var words = filter.FilterAllWords(lines,
            wordsOptions.BoringWordsThreshold);
        var tags = converter.ConvertToTags(words, wordsOptions.MinFontSize);
        var bmp = drawer.DrawCloud(tags);
        return bmp;
    }
}