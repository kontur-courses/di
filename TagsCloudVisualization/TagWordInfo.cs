using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public record TagWordInfo
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
            return new Size(
                (int) Math.Round(FontSize * Word.Length), 
                (int) Math.Round(FontSize));
        }
    }
}