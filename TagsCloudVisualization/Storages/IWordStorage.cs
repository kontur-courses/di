using System.Collections.Generic;

namespace TagsCloudVisualization.Storages
{
    public interface IWordStorage
    {
        public IEnumerable<string> Words { get; set; }
    }
}
