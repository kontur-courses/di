using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface IWordBrushProvider
    {
        Brush CreateBrushForWord(string word, int occurrenceCount);
    }
}