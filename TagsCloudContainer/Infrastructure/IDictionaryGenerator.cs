using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure
{
    internal interface IDictionaryGenerator
    {
        public Dictionary<string, int> GenerateDictionary(IEnumerable<string> lines);
    }
}