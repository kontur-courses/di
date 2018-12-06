using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class ArialFontScheme : IFontScheme
    {
        public Font Process(FrequentedWord element)
        {
            return new Font("Arial", (element.Frequency + 1) * 10);
        }
    }
}