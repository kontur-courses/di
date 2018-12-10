using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordColorPicker
    {
        Dictionary<string, Color> PickColors(List<(string word, int count)> words);

        void SetBaseWordColor(Color color);

        void SetBackgroundColor(Color color);
    }
}
