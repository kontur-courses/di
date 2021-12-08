using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordCloudCreator
    {
        IEnumerable<Word> GetWordCloud(Graphics graphic, ImageSettings imageSettings);
    }
}