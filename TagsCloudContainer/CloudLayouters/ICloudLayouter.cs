using System.Collections.Generic;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer.CloudLayouters
{
    public interface ICloudLayouter
    {
        IEnumerable<CloudVisualizationWord> GetWords(IEnumerable<CloudWord> cloudWords);
    }
}