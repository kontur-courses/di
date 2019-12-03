using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface IColorScheme
    {
        Color GetColorForCurrentWord((string word, int frequency) wordFrequency, int positionByFrequency, int countWords);
    }
}
