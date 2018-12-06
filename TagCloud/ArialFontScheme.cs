using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class ArialFontScheme : IFontScheme
    {
        public const string FamilyName = "Arial";

        public Font Process(FrequentedWord element)
        {
            return new Font(FamilyName, (element.Frequency + 1) * 10);
        }
    }
}