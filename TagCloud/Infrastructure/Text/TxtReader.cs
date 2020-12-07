using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TagCloud.Infrastructure.Settings;

namespace TagCloud.Infrastructure.Text
{
    public class TxtReader : IReader<string>
    {
        private readonly Func<IFileSettingsProvider> fileSettingsProvider;

        public TxtReader(Func<IFileSettingsProvider> fileSettingsProvider)
        {
            this.fileSettingsProvider = fileSettingsProvider;
        }

        public IEnumerable<string> ReadTokens()
        {
            var path = fileSettingsProvider().Path;
            using (var fileStream = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null) yield return line;
                }
            }
        }
    }
}