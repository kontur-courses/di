using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordColorPicker
    {
        Dictionary<string, Color> PickColors(List<(string word, int count)> words);

        Color BackgroundColor { get; }
    }
}
