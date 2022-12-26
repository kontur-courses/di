using TagCloudContainer.Models;

namespace TagCloudContainer.Interfaces
{
    public interface IFrequencyCounter
    {
        IEnumerable<TagWithFrequency> GetTagsFrequency(IEnumerable<string> words, bool useSort);
    }
}
