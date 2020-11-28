using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class CloudGenerator : ICloudGenerator
    {
        private readonly IFontSizeGetter fontSizeGetter;
        private readonly ICloudLayouter layouter;

        public CloudGenerator(IFontSizeGetter fontSizeGetter, ICloudLayouter layouter)
        {
            this.fontSizeGetter = fontSizeGetter;
            this.layouter = layouter;
        }

        public IEnumerable<Tag> GenerateCloud(Dictionary<string, double> frequencyDictionary, string fontName)
        {
            foreach (var pair in frequencyDictionary.OrderByDescending(pair => pair.Value))
            {
                var word = pair.Key;
                var frequency = pair.Value;
                var fontSize = fontSizeGetter.GetFontSize(word, frequency);
                var rectangleSize = GetRectangleSize(word, fontSize, fontName);
                var nextRectangle = layouter.PutNextRectangle(rectangleSize);
                yield return new Tag(word, fontSize, nextRectangle.Location);
            }
        }

        private Size GetRectangleSize(string word, double fontSize, string fontName)
        {
            return TextRenderer.MeasureText(word, new Font(fontName, (float) fontSize));
        }
    }
}