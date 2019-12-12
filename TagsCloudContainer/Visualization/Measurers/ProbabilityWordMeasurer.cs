using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.Data;

namespace TagsCloudContainer.Visualization.Measurers
{
    internal class ProbabilityWordMeasurer : IWordMeasurer
    {
        private readonly FontFamily fontFamily;
        private readonly float sizeFactor;

        public ProbabilityWordMeasurer(ISettings settings) : this(settings.FontFamily, settings.SizeFactor)
        {
        }

        private ProbabilityWordMeasurer(FontFamily fontFamily, float sizeFactor)
        {
            this.fontFamily = fontFamily;
            this.sizeFactor = sizeFactor;
        }

        public (Font font, Size size) Measure(Word word)
        {
            var emSize = (int) (100 * word.Probability * sizeFactor);
            var font = new Font(fontFamily, emSize);
            var size = TextRenderer.MeasureText(word.Value, font);
            return (font, size);
        }

        public interface ISettings
        {
            FontFamily FontFamily { get; }
            float SizeFactor { get; }
        }
    }
}