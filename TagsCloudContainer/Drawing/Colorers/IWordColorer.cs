using System.Drawing;

namespace TagsCloudContainer.Drawing.Colorers;

public interface IWordColorer
{
    public Color GetWordColor(string word, int wordFrequency);
}