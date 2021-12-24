using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    internal record TagWordInfo
    {
        public string Word { get; }
        private float FontSize { get; }
        
        public TagWordInfo(string word, float fontSize)
        {
            FontSize = fontSize;
            Word = word;
        }

        public Size GetCollisionSize()
        {
            var len = Word.Select(x => x.GetSize() * FontSize).Sum();
            return new Size((int) Math.Round(len), (int) Math.Round(FontSize));
        }
    }
}