using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordsTransformer
    {
        IEnumerable<ValueTuple<Size, string>> Transform();
    }
}