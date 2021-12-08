using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public record TagWordInfo
    {
        public string Word { get; }
        public float FontSize { get; }
        
        public TagWordInfo(string word, float fontSize)
        {
            FontSize = fontSize;
            Word = word;
        }

        public Size GetCollisionSize()
        {
            return new Size(
                (int) Math.Round(FontSize * Word.Length + 5), 
                (int) Math.Round(FontSize));
        }
    }
}