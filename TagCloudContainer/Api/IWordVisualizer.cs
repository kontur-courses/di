using System.Collections.Generic;
using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface IWordVisualizer
    {
        Image CreateImageWithWords(IEnumerable<string> words);
    }
}