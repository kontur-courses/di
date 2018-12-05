using System.Drawing;
using TagsCloudVisualization.Interfaces;
using Size = TagsCloudVisualization.Layouter.Size;

namespace TagsCloudVisualization
{
    public class LinearSizeScheme : ISizeScheme
    {
        public Size GetSize(FrequentedFontedWord element)
        {
            var width = element.Word.Length * element.Font.Size;
            return new Size(width, element.Font.Height);
        }
    }
}