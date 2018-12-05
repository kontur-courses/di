using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public interface ICloudLayouter
    {
        void PutNextWord(GraphicWord graphicWord);
        void Clear();
    }
}
