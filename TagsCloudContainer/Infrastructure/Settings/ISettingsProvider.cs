using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.Settings
{
    public interface ISettingsProvider
    {
        public WordColorSettings GetWordColorSettings();
        public WordFontSettings GetWordFontSettings();
        public OutputImageSettings GetOutputImageSettings();
        public TextReaderSettings GetTextReaderSettings();
    }
}