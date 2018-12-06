using System.Collections.Generic;
using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class SimpleConverter : IConverter
    {
        public IEnumerable<Tag> Convert(IEnumerable<string> strings)
        {
            foreach (var word in strings)
            {
                yield return new Tag(word,new Size(50,50));
            }
        }
    }
}