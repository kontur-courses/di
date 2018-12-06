using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IWordDataProvider
    {
        List<CloudWordData> GetData(CircularCloudLayouter cloud, List<string> words);
    }
}