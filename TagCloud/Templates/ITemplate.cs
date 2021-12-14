using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Templates
{
    public interface ITemplate
    {
        void Add(WordParameter wordParameter);
        IEnumerable<WordParameter> GetWordParameters();
        Size Size { get; set; }
        PointF Center { get; set; }
        Color BackgroundColor { get; set; }
    }
}