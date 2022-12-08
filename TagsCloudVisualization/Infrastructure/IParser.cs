using System.Collections.Generic;

namespace TagsCloudVisualization.Infrastructure
{
    public interface IParser
    {
        public string FileType { get; }

        public IEnumerable<string> WordParse(string path);
    }
}