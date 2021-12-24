using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Readers;

namespace TagsCloudVisualization
{
    public class InfoTagsCloud
    {
        public InfoTagsCloud(IEnumerable<IFileReader> readers)
        {
            this.readers = readers;
        }

        public IEnumerable<TextFormat> AvailableTextFileFormatsToRead => readers.Select(x => x.Format);
        private readonly IEnumerable<IFileReader> readers;
    }
}