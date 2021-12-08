/*
using System.Collections.Generic;

namespace TagsCloudVisualizationDI.TextAnalization.LowerCaseMaker
{
    public class LowerCaseMaker : ILowerCaseMaker
    {
        //public string MakeTextLowerCase(string text) => text.ToLower();
        public IEnumerable<Word> MakeTextLowerCase(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                word.ToLower();
                yield return word;
            }
        }
    }
}
*/