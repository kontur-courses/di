using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordDataHandler
    {
        List<CloudWordData> GetDatas(ICloudLayouter cloud, string parametersFilePath);
    }
}
