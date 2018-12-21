using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ColorAlgorithm
{
    public class StaticColorAlgorithm : IColorAlgorithm
    {
        public Color GetColor(Dictionary<string, int> words = null, string word = "")
        {
            return Color.Black;
        }
    }
}