using System.Drawing;

namespace TagCloud;

public interface ITagCloudCreatorFactory
{
    ITagCloudCreator Get(Size pictureSize, Point cloudCenter, Color[] colors, string fontName,
        int maxFontSize, string inputFile, string boringWordsFile);
}