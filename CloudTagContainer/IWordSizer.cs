using System.Collections.Generic;

namespace CloudTagContainer
{
    public interface IWordSizer
    {
        public List<SizedWord> Convert(string[] words);
    }
}