using System.Drawing;

namespace TagsCloudContainer.Colorers;

public interface IColorProvider
{
    Color ProvideColorForWord(string word, int frequency);
}