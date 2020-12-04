using System.Drawing;

namespace TagCloud.Visualization.WordsColorings
{
    public interface IWordsColoring
    {
        public Color GetColor(string word, Rectangle location, TagCloud cloud);
    }
}
