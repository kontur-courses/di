using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Interfaces
{
    public interface ITagCloudMaker
    {
        Result<string> CreateTagCloud(string filePath, int minLetterSize);
    }
}