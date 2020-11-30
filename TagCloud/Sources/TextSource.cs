using System;
using System.Collections.Generic;
using System.Text;
using TagCloud.Settings;

namespace TagCloud.Sources
{
    public class TextSource : ISource
    {
        private SourceSettings settings;

        public TextSource(SourceSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<string> Words()
        {
            throw new NotImplementedException();
        }
    }
}
