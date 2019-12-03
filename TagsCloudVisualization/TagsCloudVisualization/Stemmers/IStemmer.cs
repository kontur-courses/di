using System.Collections.Generic;


namespace TagsCloudVisualization.Stemmers
{
    interface IStemmer
    {
        IEnumerable<(string value, string valueForFilter)> GetStemmedString();
    }
}
