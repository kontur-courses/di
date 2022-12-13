using System.Diagnostics;
using System.Text.RegularExpressions;
using TagsCloudContainer.Core.Options.Interfaces;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.WordsParser
{
    public class WordsFilter : IWordsFilter
    {        
        private static readonly Regex MatchWordsInfo = new(@"(?<wordInfo>\w+{.*?})");
        private static readonly List<string> BoringPartsOfSpeech = new() { "=CONJ", "=PR", "=INTJ", "=PART" };

        private readonly string? _myStemLocation;
        private readonly HashSet<string> _boringWords;

        public WordsFilter(IFilterOptions options)
        {            
            _myStemLocation = options.MyStemLocation;
            _boringWords = options.BoringWords
                .Select(word => word.Normalize())
                .ToHashSet();
        }

        public HashSet<string> RemoveBoringWords(HashSet<string> words)
        {
            var wordsInfo = GetWordsInfo(words.ToHashSet(), _myStemLocation);
            var filteredWords = words
                .Where(word => !_boringWords.Contains(word) && !IsWordBoringPartOfSpeech(word, wordsInfo))
                .ToHashSet();

            return filteredWords;
        }

        private static IEnumerable<string> GetWordsInfo(IEnumerable<string> words, string? myStemLocation)
        {
            if (myStemLocation is null)
                return words;

            var myStem = new Process
            {
                StartInfo =
                {
                    FileName = Path.Combine(myStemLocation, "mystem.exe"),
                    Arguments = "-i -n -e cp866",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };

            myStem.Start();

            foreach (var word in words)
                myStem.StandardInput.Write($"{word}\n");

            myStem.StandardInput.Close();

            var wordsInfo = MatchWordsInfo
                .Matches(myStem.StandardOutput.ReadToEnd())
                .Select(m => m.Groups["wordInfo"].Value)
                .ToList();

            myStem.WaitForExit();
            myStem.Close();
            myStem.Dispose();

            return wordsInfo;
        }

        private static bool IsWordBoringPartOfSpeech(string word, IEnumerable<string> wordsInfo)
        {
            var boringWordInfos = wordsInfo
                .Where(wordInfo => wordInfo.Contains(word))
                .Where(wordInfo => BoringPartsOfSpeech.Any(wordInfo.Contains))
                .ToList();

            return boringWordInfos.Count != 0;
        }
    }
}
