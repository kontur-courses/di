using System.Collections.Generic;
using System.Linq;
using MyStemWrapper;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class GrammemeChecker : IWordChecker
    {
        private readonly HashSet<string> boringGrammemes = new HashSet<string>
        {
            "CONJ", "INTJ", "PART", "PR", "APRO", "SPRO", "ADVPRO"
        };

        private readonly MyStem myStem;

        public GrammemeChecker(MyStem myStem)
        {
            this.myStem = myStem;
            myStem.Parameters = "-i";
        }

        public bool IsWordNotBoring(string word)
        {
            var wordGrammeme = new string(myStem.Analysis(word)
                .SkipWhile(x => x != '=')
                .Skip(1)
                .TakeWhile(char.IsLetter)
                .ToArray());
            return !boringGrammemes.Contains(wordGrammeme);
        }
    }
}