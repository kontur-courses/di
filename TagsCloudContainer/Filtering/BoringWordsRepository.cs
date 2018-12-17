using System.Collections.Generic;
using TagsCloudContainer.Reading;

namespace TagsCloudContainer.Filtering
{
    public class BoringWordsRepository : IBoringWordsRepository
    {
        public IEnumerable<string> Words { get; private set; }


        public void LoadWords(string inputPath)
        {
            Words = new TxtWordsReader().ReadWords(inputPath);
        }
    }
}