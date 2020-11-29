using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Filters;

namespace TagCloud.Infrastructure.Text
{
    public class LineParser : IParser<string>
    {
        private readonly Func<IFileSettingsProvider> fileSettingsProvider;
        private readonly IEnumerable<IFilter<string>> filters;

        public LineParser(Func<IFileSettingsProvider> fileSettingsProvider, IEnumerable<IFilter<string>> filters)
        {
            this.fileSettingsProvider = fileSettingsProvider;
            this.filters = filters;
        }
        
        public IEnumerable<string> Parse()
        {
            var path = fileSettingsProvider().Path;
            return filters.Aggregate(
                ReadLines(path), 
                (current, filter) => filter.Filter(current));
        }

        private IEnumerable<string> ReadLines(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}