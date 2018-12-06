using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface IFontScheme
    {
        Font Process(FrequentedWord element);
    }
}