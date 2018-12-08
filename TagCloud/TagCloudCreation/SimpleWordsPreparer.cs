using System.Collections.Generic;
using System.Linq;

namespace TagCloudCreation
{
    public class SimpleWordsPreparer : IWordsPreparer
    {
        private readonly HashSet<string> boringWords = new HashSet<string> {""};

        public List<WordInfo> PrepareWords(List<WordInfo> stats) =>
            stats.Where(wi => !boringWords.Contains(wi.Word))
                 .ToList();
    }
}
