using System.Drawing;

namespace TagsCloudContainer.Colorers;

public interface IColorer
{
    Color ProvideColorForWord(string word, int frequency);
}