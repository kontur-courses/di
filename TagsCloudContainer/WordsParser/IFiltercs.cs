using System.Collections.Generic;

namespace TagsCloudContainer.WordsParser
{
    public interface ISettings
    {
        public IEnumerable<string> BoringWords { get; }
    }
}