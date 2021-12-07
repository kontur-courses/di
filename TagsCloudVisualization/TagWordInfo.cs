using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public record TagWordInfo
    {
        public string Word { get; }
        public double FontSize { get; }
        
        public TagWordInfo(string word, double fontSize)
        {
            FontSize = fontSize;
            Word = word;
        }

        public Size GetCollisionSize()
        {
            return new Size(
                (int) Math.Round(FontSize), 
                (int) Math.Round(FontSize * Word.Length));
        }
    }
}