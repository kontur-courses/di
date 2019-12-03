using System.Collections.Generic;
using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface IWordCloudLayouter : ILayoutProvider
    {
        IReadOnlyDictionary<string, Rectangle> AddWords(IReadOnlyDictionary<string, int> words);
    }
}