using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class WordFontSizeProvider : IWordFontSizeProvider
    {
        public float GetFontSize(string word)
        {
            return 14F;
        }
    }
}