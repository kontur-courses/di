using System.Drawing;

namespace TagCloudPainter.Sizers;

public interface IWordSizer
{
    Size GetTagSize(string word, int count);
}