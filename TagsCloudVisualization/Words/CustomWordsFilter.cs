using System.Diagnostics;
using System.Text;
using TagsCloudVisualization.MorphAnalyzer;

namespace TagsCloudVisualization.Words;

public class CustomWordsFilter : IWordsFilter
{
    private readonly IMorphAnalyzer _morphAnalyzer;

    public CustomWordsFilter(IMorphAnalyzer morphAnalyzer)
    {
        _morphAnalyzer = morphAnalyzer;
    }

    public Dictionary<string, int> FilterWords(Dictionary<string, int> wordsAndCount, VisualizationOptions options)
    {
        var speechInfo = _morphAnalyzer.GetWordsMorphInfo(wordsAndCount.Keys);
        var filteredWords = new Dictionary<string, int>();
        foreach (var wordCountPair in wordsAndCount)
        {
            if (options.BoringWords.Contains(wordCountPair.Key))
                continue;

            if (!options.ExcludedPartsOfSpeech.Any() || !speechInfo.TryGetValue(wordCountPair.Key, out var wordMorphInfo))
            {
                filteredWords.Add(wordCountPair.Key, wordCountPair.Value);
                continue;
            }

            var needToSkip = false;
            foreach (var speechToExclude in options.ExcludedPartsOfSpeech)
            {
                if (wordMorphInfo.PartsOfSpeech.Contains(speechToExclude))
                {
                    needToSkip = true;
                    break;
                }
            }

            if (!needToSkip)
                filteredWords.Add(wordCountPair.Key, wordCountPair.Value);
        }

        return filteredWords;
    }
}