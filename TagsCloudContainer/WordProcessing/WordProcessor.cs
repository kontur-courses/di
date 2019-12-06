using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.WordProcessing
{
    public class WordProcessor
    {
        private readonly MyStem myStem;
        public WordProcessor(MyStem myStem)
        {
            this.myStem = myStem;
        }
        
        public IEnumerable<WordData> Process(string inputTxtFile)
        {
            var counter = new Dictionary<string, int>();
            var outputFile = $"{myStem.WorkingDirectory}\\output.txt";
            myStem.ProcessWords(inputTxtFile, outputFile);
            using (var stream = new StreamReader(outputFile))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    var word = Parse(line);
                    if (!counter.ContainsKey(word))
                        counter[word] = 0;
                    counter[word] += 1;
                }
            }
            return counter.Select(w => new WordData(w.Key, w.Value));
        }

        private static string Parse(string line)
        {
            var match =  Regex.Match(line, @"{([a-zA-Zа-яА-Я]+)(\?.*)?}").Groups;
            return match[1].Value;
        }
    }
}