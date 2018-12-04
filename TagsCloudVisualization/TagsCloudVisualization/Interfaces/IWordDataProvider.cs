using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IWordDataProvider
    {
        List<CloudWordData> GetData(ICloudLayouter cloud, List<string> words);
    }
}
