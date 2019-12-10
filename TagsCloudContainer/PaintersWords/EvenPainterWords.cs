using System.Drawing;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.PaintersWords
{
    public class EvenPainterWords : IPainterWords
    {
        public Brush GetNextBrush(WordToken wordToken)
        {
            if (wordToken.Count % 2 == 0)
                return Brushes.Red;
            return Brushes.Aqua;
        }
    }
}
