using System.Collections.Generic;


namespace TagsCloudVisualization.TextAnalization.NormalizationMaker
{
    public class NormalizationMaker : INormalizationMaker
    {
        public IEnumerable<Word> MakeNormalization(IEnumerable<Word> words)
        {
            //приводим слово к начальной форме
            /*
            foreach (var word in words)
            {
                yield return word;
            }
            */

            return words;
        }
    }
}
