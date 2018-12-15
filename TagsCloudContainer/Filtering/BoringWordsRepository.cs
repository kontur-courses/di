using System.Collections.Generic;
using TagsCloudContainer.Reading;

namespace TagsCloudContainer.Filtering
{
    public class BoringWordsRepository : IBoringWordsRepository
    {
        public IEnumerable<string> Words { get; }

        public BoringWordsRepository(string inputPath)
        {
            Words = new TxtWordsReader().ReadWords(new ReadingSettings(inputPath));
        }
    }
}