using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.ImageProcessing
{
    public interface ICloudImageGenerator
    {
        Bitmap GenerateBitmap(IReadOnlyDictionary<string, double> wordsFrequencies);
    }
}
