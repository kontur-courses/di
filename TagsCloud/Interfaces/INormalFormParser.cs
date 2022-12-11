using DeepMorphy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface INormalFormParser
    {
        public IEnumerable<string> Normalize(IEnumerable<MorphInfo> clearMorphs);
    }
}
