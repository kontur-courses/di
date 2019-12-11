using System.Collections.Generic;
using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface IWordCloudLayouter
    {
        IReadOnlyDictionary<string, Rectangle> AddWords(IReadOnlyDictionary<string, int> words,
            List<Rectangle> containter);
    }
}