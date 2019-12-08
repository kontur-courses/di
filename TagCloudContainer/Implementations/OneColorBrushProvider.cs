using System.Drawing;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    public class OneColorBrushProvider : IWordBrushProvider
    {
        public Brush CreateBrushForWord(string word, int occurrenceCount)
        {
            return new SolidBrush(Color.Orange);
        }
    }
}