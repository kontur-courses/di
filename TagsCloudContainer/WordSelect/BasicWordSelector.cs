using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class BasicWordSelector : IWordSelector
    {
        public string Select(string word)
        {
            return word.Length < 4 ? null : word;
        }
    }
}