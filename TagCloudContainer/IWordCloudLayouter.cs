using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace TagCloudContainer
{
    public interface IWordCloudLayouter
    {
        IEnumerable<(string word, Rectangle wordRectangle)> AddWords(
            IEnumerable<(string word, int occurrenceCount)> words);
    }
}