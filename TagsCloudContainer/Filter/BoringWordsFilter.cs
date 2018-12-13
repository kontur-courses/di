using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.DataReader;

namespace TagsCloudContainer.Filter
{
    public class BoringWordsFilter : IFilter
    {
        private HashSet<string> BoringWords { get; }

        public BoringWordsFilter(IBoringWordsFilterSettings settings, IDataReader fileReader)
        {
            BoringWords = new HashSet<string>(fileReader.Read(settings.BoringWordsFileName));
        }

        public IEnumerable<string> FilterOut(IEnumerable<string> words)
        {
            return words.GroupBy(word => word)
                .Where(group => !BoringWords.Contains(group.Key))
                .SelectMany(group => group);
        }
    }
}