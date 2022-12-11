using DeepMorphy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface IMorphsFilter
    {
        public IEnumerable<MorphInfo> FilterRedundantWords(IEnumerable<MorphInfo> morphs);
    }
}
