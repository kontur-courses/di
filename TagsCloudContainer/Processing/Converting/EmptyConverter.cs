using System.Collections.Generic;

namespace TagsCloudContainer.Processing.Converting
{
    // класс заглушка
    public class EmptyConverter : IWordConverter
    {
        public IEnumerable<string> Convert(IEnumerable<string> words)
        {
            return words;
        }
    }
}
