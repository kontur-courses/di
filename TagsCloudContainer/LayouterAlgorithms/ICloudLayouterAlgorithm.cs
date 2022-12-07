using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.LayouterAlgorithms
{
    public interface ICloudLayouterAlgorithm
    {
        public Dictionary<Tuple<string, int>, Rectangle> GetWordRectangleDictionary();
    }
}