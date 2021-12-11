using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IPaintConfig
    {
        List<Brush> WordsColors { get;}
        Font Font { get;}

        Size ImageSize { get; }
    }
}
