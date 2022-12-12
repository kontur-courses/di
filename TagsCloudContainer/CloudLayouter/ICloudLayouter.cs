using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudLayouter
    {
        ICloudItem PutNextCloudItem(string word, Size size, Font font);
        IList<ICloudItem> Items { get; }
    }
}