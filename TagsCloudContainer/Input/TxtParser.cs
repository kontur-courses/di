using System;
using System.Collections.Generic;

namespace TagsCloudContainer.Input
{
    public class TxtParser : IWordParser
    {
        public IEnumerable<string> ParseWords(string input) =>
            input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
    }
}
