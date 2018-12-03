using System.Collections.Generic;


namespace TagsCloudVisualization.Preprocessors
{
    public interface IFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}
