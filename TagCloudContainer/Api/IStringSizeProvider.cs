using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface IStringSizeProvider
    {
        Size GetStringSize(string word, int occurrenceCount);
    }
}