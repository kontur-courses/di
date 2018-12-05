using System.Drawing;
using TagsCloudVisualization.Interfaces;
using Size = TagsCloudVisualization.Layouter.Size;

namespace TagsCloudVisualization
{
    public class LinearSizeScheme : ISizeScheme
    {
        private const double CharWidth = 25;
        private const double CharHeight = 40;

        public Size GetSize(FrequentedFontedWord element)
        {
            var width = element.Word.Length * CharWidth * element.Frequency;
            return new Size(width, CharHeight * element.Frequency);
        }
    }
}