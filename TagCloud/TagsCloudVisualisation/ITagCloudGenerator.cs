using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Formatting;

namespace TagsCloudVisualisation
{
    public interface ITagCloudGenerator : IDisposable
    {
        Task<Image> DrawWordsAsync(IFontSizeResolver fontSizeResolver,
            Color[] palette,
            ITagCloudLayouter layouter,
            WordWithFrequency[] wordsCollection,
            CancellationToken token, FontFamily fontFamily);
    }
}