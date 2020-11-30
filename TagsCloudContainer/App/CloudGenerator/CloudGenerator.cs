using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class CloudGenerator : ICloudGenerator
    {
        private readonly IFontGetter fontGetter;
        private readonly ICloudLayouter layouter;

        public CloudGenerator(IFontGetter fontGetter, ICloudLayouter layouter)
        {
            this.fontGetter = fontGetter;
            this.layouter = layouter;
        }

        public IEnumerable<Tag> GenerateCloud(Dictionary<string, double> frequencyDictionary)
        {
            foreach (var pair in frequencyDictionary.OrderByDescending(pair => pair.Value))
            {
                var word = pair.Key;
                var frequency = pair.Value;
                var font = fontGetter.GetFont(word, frequency);
                var rectangleSize = GetRectangleSize(word, font);
                var nextRectangle = layouter.PutNextRectangle(rectangleSize);
                yield return new Tag(word, font.Size, nextRectangle.Location);
            }
        }

        private Size GetRectangleSize(string word, Font font)
        {
            return TextRenderer.MeasureText(word, font);
        }
    }
}