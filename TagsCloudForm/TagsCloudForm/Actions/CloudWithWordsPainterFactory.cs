using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm.Actions
{
    public class CloudWithWordsPainterFactory
    {
        public CloudWithWordsPainter Create(IImageHolder imageHolder,
            CircularCloudLayouterSettings settings, Palette palette, ICircularCloudLayouter layouter, Dictionary<string, int> words)
        {
            return new CloudWithWordsPainter(imageHolder, settings, palette, layouter, words);
        }
    }
}
