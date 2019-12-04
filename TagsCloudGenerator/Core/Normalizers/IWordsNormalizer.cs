using System.Collections.Generic;
using NHunspell;

namespace TagsCloudGenerator.Core.Normalizers
{
    public interface IWordsNormalizer
    {
        IEnumerable<string> GetNormalizedWords(IEnumerable<string> text, HashSet<string> boringWords,
            Hunspell hunspell);
    }
}