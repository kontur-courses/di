using System.Drawing;

namespace TagCloud2.TextGeometry
{
    public interface IStringToSizeConverter
    {
        public Size Convert(string input, Font font);
    }
}
