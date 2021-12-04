using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Layouters;

namespace TagCloud.Visualizers
{
    public interface IVisualizer : IDisposable
    {
        Bitmap DrawCloud(IEnumerable<Tag> tags);
    }
}