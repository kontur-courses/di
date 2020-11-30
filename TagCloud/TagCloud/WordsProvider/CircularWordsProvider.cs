using System.Collections.Generic;
using TagCloud;

namespace TagCloudTest
{
    public class CircularWordsProvider : IWordsProvider
    {
        public IEnumerable<string> GetWords()
        {
            while (true)
            {
                yield return "word";
                yield return "another word";
            }
        }
    }
}