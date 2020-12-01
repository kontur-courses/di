using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IWordConverter
    {
        public List<ICloudTag> ConvertWords(List<string> words);
    }
}