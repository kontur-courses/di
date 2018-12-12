using System.Linq;
using System.Text.RegularExpressions;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class WordFilter : IWordFilter
    {
        private readonly bool ignoreBoring;
        private readonly string[] ignoringPartsOfSpeech;
        private readonly Regex parsePartOfSpeechRegex;
        private readonly string[] stopWords;

        public WordFilter(
            IFileReader fileReader,
            string path,
            bool ignoreBoring)
        {
            stopWords = path == "" ? new string[0] : fileReader.Read(path).ToArray();
            this.ignoreBoring = ignoreBoring;
            parsePartOfSpeechRegex = new Regex(@"\w*?=(\w+)");
            ignoringPartsOfSpeech = new[]
            {
                "PR",
                "PART",
                "CONJ",
                "SPRO",
                "ADVPRO",
                "APRO"
            };
        }

        public bool ToExclude(string word)
        {
            return stopWords.Contains(word) || ignoreBoring && IsBoring(word);
        }

        private bool IsBoring(string word)
        {
            var output = ProgramExecuter.RunProgram(@"mystem.exe", "-l -i", word);
            var partOfSpeech = parsePartOfSpeechRegex.Match(output).Groups[1].Value;
            return ignoringPartsOfSpeech.Contains(partOfSpeech);
        }
    }
}