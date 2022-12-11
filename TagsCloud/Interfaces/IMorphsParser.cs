using DeepMorphy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface IMorphsParser
    {
        public IEnumerable<MorphInfo> GetMorphs(string filePath);
    }
}
