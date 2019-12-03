using System.Collections.Generic;
using System.IO;

namespace TagCloud
{
    public static class ExtracterHelper
    {
        public static HashSet<string> GetBoringWords() =>
            new HashSet<string>(File.ReadAllLines("BoringWords.txt"));
    }
}
