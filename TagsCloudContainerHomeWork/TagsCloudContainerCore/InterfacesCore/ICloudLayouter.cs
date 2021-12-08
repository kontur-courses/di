using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainerCore.InterfacesCore
{
    public interface ICloudLayouter
    {
        void AddTags(IEnumerable<string> words);
        IEnumerator<TagToRender> GetWordsToRender();
    }
}