using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp.ToSizeConverter
{
    public interface IToSizeConverter
    {
        IEnumerable<Tuple<string, Size>> ConvertToSizes(IEnumerable<string> words);
    }
}
