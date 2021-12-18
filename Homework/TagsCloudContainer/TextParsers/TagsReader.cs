using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public class TagsReader : ISourceReader
    {
        private readonly IEnumerable<string> tags;
        public TagsReader(IEnumerable<string> tags)
        {
            this.tags = tags;
        }

        public IEnumerable<string> GetNextWord()
        {
            foreach (var tag in tags)
                yield return tag;
        }
    }
}
