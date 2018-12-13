using System.Collections.Generic;


namespace TagsCloudVisualization.Preprocessing
{
    public interface IFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}
