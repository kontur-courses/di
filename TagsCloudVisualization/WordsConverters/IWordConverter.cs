using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordConverter
    {
        public List<ICloudTag> ConvertWords(List<string> words);
    }
}