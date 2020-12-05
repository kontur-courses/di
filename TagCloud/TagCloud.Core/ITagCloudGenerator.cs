using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TagCloud.Core.Layouting;
using TagCloud.Core.Text.Formatting;

namespace TagCloud.Core
{
    public interface ITagCloudGenerator : IDisposable
    {
        Task<Image> DrawWordsAsync(IFontSizeResolver fontSizeResolver,
            Color[] palette,
            ITagCloudLayouter layouter,
            Dictionary<string, int> wordsCollection,
            CancellationToken token, FontFamily fontFamily);
    }
}