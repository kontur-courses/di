using System.Drawing;

namespace TagCloud.Templates.Colors
{
    public interface IColorGenerator
    {
        public Color GetColor(string word);
    }
}