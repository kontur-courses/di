using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IPaintConfig
    {
        List<Pen> WordsColors { get;}
        Font WordsFont { get;}

        Size ImageSize { get; }
    }
}
