using System.Drawing;
using TagCloud.Interfaces;
using TagCloud.IntermediateClasses;

namespace TagCloud
{
    public class ArialFontScheme : IFontScheme
    {
        public const string FamilyName = "Arial";
        public const int Size = 16;

        public Font Process(PositionedElement element)
        {
            return new Font(FamilyName, Size);
        }
    }
}