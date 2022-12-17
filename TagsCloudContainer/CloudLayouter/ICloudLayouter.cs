using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.CloudItem;

namespace TagsCloudContainer.CloudLayouter
{
    public interface ICloudLayouter
    {
        public IList<ICloudItem> Items { get; }
        public ICloudItem PutNextCloudItem(string word, Size size, Font font);
    }
}