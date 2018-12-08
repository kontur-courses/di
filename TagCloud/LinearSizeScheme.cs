using System.Drawing;
using TagCloud.Interfaces;
using TagCloud.IntermediateClasses;
using Size = TagCloud.Layouter.Size;

namespace TagCloud
{
    public class LinearSizeScheme : ISizeScheme
    {
        public Size GetSize(FrequentedWord word)
        {
            var width = word.Word.Length * word.Frequency * 5 + 100;
            var height = word.Frequency * 5 + 50;
            return new Size(width, height);
        }
    }
}