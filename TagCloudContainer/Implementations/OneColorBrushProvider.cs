using System.Drawing;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    [CliElement("onecolorbrush", typeof(OneColorBrushProvider))]
    public class OneColorBrushProvider : IWordBrushProvider
    {
        private readonly DrawingOptions options;

        public OneColorBrushProvider(DrawingOptions options)
        {
            this.options = options;
        }

        public Brush CreateBrushForWord(string word, int occurrenceCount)
        {
            return options.WordBrush;
        }
    }
}