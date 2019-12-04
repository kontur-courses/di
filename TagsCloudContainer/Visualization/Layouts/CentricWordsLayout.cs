using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.Core.Layouters;
using TagsCloudContainer.Data;

namespace TagsCloudContainer.Visualization.Layouts
{
    internal class CentricWordsLayout : IWordsLayout
    {
        private readonly FontFamily fontFamily;
        private readonly float sizeFactor;

        internal CentricWordsLayout(FontFamily fontFamily, float sizeFactor)
        {
            this.fontFamily = fontFamily;
            this.sizeFactor = sizeFactor;
        }

        public IEnumerable<Tag> PlaceWords(CircularCloudLayouter layouter, IEnumerable<Word> words)
        {
            return words.Select(word => CreateTag(layouter, word));
        }

        private Tag CreateTag(CircularCloudLayouter layouter, Word word)
        {
            var emSize = (int) (100 * word.Probability * sizeFactor);
            var font = new Font(fontFamily, emSize);
            var size = TextRenderer.MeasureText(word.Value, font);
            var rectangle = layouter.PutNextRectangle(size);
            return new Tag(word.Value, font, rectangle);
        }
    }
}