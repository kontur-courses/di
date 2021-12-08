using System.Collections.Generic;


namespace TagsCloudVisualizationDI.TextAnalization.NormalizationMaker
{
    public class DefaultNormalizationMaker : INormalizationMaker
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
