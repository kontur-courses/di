using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Layouter;

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