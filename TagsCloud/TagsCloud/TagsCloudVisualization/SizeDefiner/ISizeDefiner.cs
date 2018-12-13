using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.TagsCloudVisualization
{
    public interface ISizeDefiner
    {
        Tuple<Size,int> GetSizeAndFont(string word, int frequency, int maxFrequency, int minFrequency);
    }
}
