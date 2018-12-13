using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.DataReader;

namespace TagsCloudContainer.Filter
{
    public class BoringWordsFilter : IFilter
    {
        private HashSet<string> BoringWords { get; }

        public BoringWordsFilter(IConfiguration configuration, IDataReader fileReader)
        {
            BoringWords = new HashSet<string>(fileReader.Read(configuration.BoringWordsFileName));
        }

        public IEnumerable<string> Filtrate(IEnumerable<string> words)
        {
            return words.GroupBy(word => word)
                .Where(group => !BoringWords.Contains(group.Key))
                .SelectMany(group => group);
        }
    }
}