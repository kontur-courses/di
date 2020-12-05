using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TagCloud.Core.Layouting;
using TagCloud.Core.Text;
using TagCloud.Core.Text.Formatting;

namespace TagCloud.Core
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