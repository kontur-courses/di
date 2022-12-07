using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.TagCloudVisualizations
{
    public interface ITagCloudVisualization
    {
        public void Visualize();
        public void Save(string file);
    }
}
