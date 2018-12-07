using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ICloudLayouter
    {
        void AddWordsFromDictionary(Dictionary<string, int> words);
        IReadOnlyDictionary<Rectangle, string> Rectangles { get; }
    }
}