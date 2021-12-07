using System.Collections.Generic;

namespace CloudTagContainer
{
    public interface IWordConverter
    {
        public List<IWord> Convert(string[] words);
    }
}