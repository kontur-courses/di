using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.WordProcessor
{
    public class BasicWordProcessor : IWordProcessor
    {
        private readonly HashSet<string> boringPartsOfSpeech = new HashSet<string>
        {
            "ADVPRO", "APRO", "CONJ", "INTJ", "PART", "PR", "SPRO"
        };

        public IEnumerable<WordWithCount> ProcessWords(IEnumerable<string> words)
        {
            var processedWords = new List<string>();
            var inputName = "words_for_mystem_in.txt";
            var outputName = "words_for_mystem_out.txt";
            File.WriteAllText(inputName, string.Join(" ", words));
            var process = Process.Start("mystem.exe", $"-n -i -d -g {inputName} {outputName}");
            if (process == null)
                throw new Exception("mystem.exe not found");
            process.WaitForExit();
            var wordAnalyzes = File.ReadAllLines(outputName);
            var analysisRegex = new Regex(@"^\w+{(\w+)\??=(\w+)[,=]", RegexOptions.Compiled);
            foreach (var analysis in wordAnalyzes)
            {
                var match = analysisRegex.Match(analysis);
                var initialForm = match.Groups[1].Value;
                var partOfSpeech = match.Groups[2].Value;
                if (!boringPartsOfSpeech.Contains(partOfSpeech)) 
                    processedWords.Add(initialForm);
            }

            return GetWordsWithCount(processedWords);
        }

        private IEnumerable<WordWithCount> GetWordsWithCount(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 0;
                frequencyDictionary[word]++;
            }

            return frequencyDictionary
                .OrderByDescending(p => p.Value)
                .Select(p => new WordWithCount(p.Key, p.Value));
        }
    }
}
