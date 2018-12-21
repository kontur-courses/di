using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ColorAlgorithm
{
    public interface IColorAlgorithm
    {
        Color GetColor(Dictionary<string, int> words = null, string word = "");
    }
}
