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
        public WordFontSizeSettings GetWordFontSizeSettings();
        public SaveTagsCloudSettings GetSaveTagsCloudSettings();
        public TextReaderSettings GetTextReaderSettings();
    }
}