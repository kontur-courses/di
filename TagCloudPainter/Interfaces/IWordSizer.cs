using System.Drawing;

namespace TagCloudPainter.Interfaces;

public interface IWordSizer
{
    Size GetTagSize(string word, int count);
}