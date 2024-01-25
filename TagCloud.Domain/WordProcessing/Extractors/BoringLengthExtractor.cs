using TagCloud.Domain.WordProcessing.Interfaces;

namespace TagCloud.Domain.WordProcessing.Extractors;

public class BoringLengthExtractor : IWordExtractor
{
    public bool IsSuitable(string word) => word.Length > 3;
}