using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.TextAnalization.LowerCaseMaker
{
    public class LowerCaseMaker : ILowerCaseMaker
    {
        //public string MakeTextLowerCase(string text) => text.ToLower();
        public IEnumerable<Word> MakeTextLowerCase(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                yield return word.ToLower();
            }
        }
    }
}
