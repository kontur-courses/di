using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.Settings
{
    public class WordFontSettings
    {
        public string FontFamily { get; set; }    

        public WordFontSizeSettings FontSizeSettings { get; set; }
    }
}