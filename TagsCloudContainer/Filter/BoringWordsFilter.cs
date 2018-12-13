using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.DataReader;

namespace TagsCloudContainer.Filter
{
    public class BoringWordsFilter : IFilter
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsFilter(IBoringWordsFilterSettings settings, IDataReader fileReader)
        {
            boringWords = new HashSet<string>(fileReader.Read(settings.BoringWordsFileName));
        }

        public IEnumerable<string> FilterOut(IEnumerable<string> words)
        {
            return words.GroupBy(word => word)
                .Where(group => !boringWords.Contains(group.Key))
                .SelectMany(group => group);
        }
    }
}