using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CliWrap;
using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public class MyStemBasedWordClassIdentifier : IWordClassIdentifier
    {
        private readonly string myStemPath;

        private static readonly Dictionary<string, WordClass> WordClassTokens = new Dictionary<string, WordClass>
        {
            {"A", WordClass.Adjective },
            {"ADV", WordClass.Adverb },
            {"S", WordClass.Noun },
            {"V", WordClass.Verb },
            {"ADVPRO", WordClass.Pronoun },
            {"APRO", WordClass.Pronoun },
            {"SPRO", WordClass.Pronoun },
            {"PR", WordClass.Preposition },
            {"CONJ", WordClass.Conjunction },
            {"INTJ", WordClass.Interjection },
            {"ANUM", WordClass.Numeral },
            {"NUM", WordClass.Numeral },
            {"PART", WordClass.Particle }
        };

        private static readonly string Pattern = ConstructPattern(WordClassTokens.Keys);

        public MyStemBasedWordClassIdentifier(string myStemPath)
        {
            this.myStemPath = myStemPath;
        }

        public WordClass GetWordClass(string word)
        {
            var myStemResult = GetMyStemResult(word, "-il");
            var wordClassToken = GetWordClassToken(myStemResult);
            return WordClassTokens[wordClassToken];
        }

        private string GetMyStemResult(string word, string arguments)
        {
            return Cli.Wrap(myStemPath)
                .SetStandardInput(word, Encoding.UTF8)
                .SetArguments(arguments)
                .SetStandardOutputEncoding(Encoding.UTF8)
                .Execute()
                .StandardOutput;
        }

        public static string GetWordClassToken(string myStemResult)
        {
            return Regex.Match(myStemResult, Pattern).Groups[1].Value;
        }

        private static string ConstructPattern(IEnumerable<string> possibleWordClasses)
        {
            return $"^{{[\\w\\?]+=({string.Join("|", possibleWordClasses.OrderByDescending(c => c.Length))})";
        }
    }
}
