using System.Collections.Generic;

namespace TagsCloudApp.Parsers
{
    public interface IKeyValueParser
    {
        IEnumerable<KeyValuePair<string, string>> Parse(string input);
    }
}