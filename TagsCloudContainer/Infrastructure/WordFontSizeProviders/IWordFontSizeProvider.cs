using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.WordFontSizeProviders
{
    public interface IWordFontSizeProvider
    {
        public float GetFontSize(string word);
    }
}