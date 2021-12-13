using System.Collections.Generic;

namespace Visualization
{
    public interface IWordSizer
    {
        public List<SizedWord> Convert(string[] words, float fontSize);
    }
}