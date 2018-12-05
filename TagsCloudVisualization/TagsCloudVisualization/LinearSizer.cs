using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class LinearSizer : ISizeDefiner
    {
        private readonly int shift;

        public LinearSizer()
        {
            shift = 8;
        }

        public LinearSizer(int shift)
        {
            this.shift = shift;
        }

        public Size GetSize(GraphicWord graphicWord)
        {
            return new Size(graphicWord.Value.Length * (graphicWord.Rate + shift), graphicWord.Rate + shift);
        }
    }
}
