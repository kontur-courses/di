using System.Collections.Generic;
using System.IO;
using TagCloud.Settings;

namespace TagCloud.Sources
{
    public class TextSource : ISource
    {
        private readonly SourceSettings settings;

        public TextSource(SourceSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<string> Words()
        {
            var words = File.ReadAllLines(settings.Destination);
            var ignore = new HashSet<string>(settings.Ignore);
            foreach (var word in words)
            {
                if (ignore.Contains(word))
                    continue;
                yield return word;
            }
        }
    }
}
