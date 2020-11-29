using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public abstract class TagCloud : ITagCloud
    {
        public void GenerateTagCloud()
        {
            var words = WordsProvider.GetTokens();
            foreach (var word in words)
            {
                
            }
        }

        public abstract Rectangle PutNextRectangle(Size rectangleSize);

        public List<Rectangle> Rectangles { get; }
        public IWordsProvider WordsProvider { get; }
    }
}