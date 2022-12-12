using System.Collections.Generic;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualization.Storages
{
    public class FilteredWordStorage : IWordStorage
    {
        public IEnumerable<string> Words { get; set; }

        public FilteredWordStorage(ITextReader reader, IWordProcessor filter)
        {
            Words = filter.Process(reader.Read());
        }
    }
}
