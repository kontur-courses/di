using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface IBitmapper
    {
        public void DrawWords(SortedDictionary<double, List<string>> words);
        public void SaveFile(string directory);
    }
}
