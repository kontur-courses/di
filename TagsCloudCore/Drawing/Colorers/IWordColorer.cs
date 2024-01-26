using System.Drawing;

namespace TagsCloudCore.Drawing.Colorers;

public interface IWordColorer
{
    public Color GetWordColor(string word, int wordFrequency);
}