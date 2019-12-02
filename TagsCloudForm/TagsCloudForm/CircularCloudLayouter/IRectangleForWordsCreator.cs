using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm
{
    public interface IRectangleForWordsCreator
    {
        Dictionary<string, Size> CreateRectanglesForWords(Dictionary<string, int> words);
    }
}
