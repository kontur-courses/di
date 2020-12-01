using System.Collections.Generic;

namespace TagsCloudContainer.WordsParser
{
    public class Settings : ISettings
    {
        public IEnumerable<string> BoringWords { get; }
        
        public Settings(IOptions options)
        {
            BoringWords = options.BoringWords;
        }
    }
}