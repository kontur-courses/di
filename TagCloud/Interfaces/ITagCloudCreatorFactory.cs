using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public interface ITagCloudCreatorFactory
    {
        ITagCloudCreator Get(Size pictureSize, Point cloudCenter, Color[] colors, string fontName,
            int maxFontSize, string inputFile, string boringWordsFile);
    }
}