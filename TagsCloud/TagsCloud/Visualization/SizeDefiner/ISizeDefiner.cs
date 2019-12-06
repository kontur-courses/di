using System;
using System.Drawing;
using TagsCloud.Visualization.Tag;

namespace TagsCloud.Visualization.SizeDefiner
{
    public interface ISizeDefiner
    {
        TagSize GetTagSize(string word, int frequency, int maxFrequency, int minFrequency);
    }
}