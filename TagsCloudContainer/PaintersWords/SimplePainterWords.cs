using System.Drawing;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.PaintersWords
{
    public class SimplePainterWords : IPainterWords
    {
        private Brush brush;

        public SimplePainterWords(Brush brush)
        {
            this.brush = brush;
        }

        public Brush GetNextBrush(WordToken wordToken)
        {
            return brush;
        }
    }
}
