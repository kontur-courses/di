using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using TagsCloudVisualization.GUI;

namespace TagsCloudVisualization.Settings
{
    public class Restrictions
    {
        [Description("Amount of words to show on Tag Cloud")]
        public int AmountOfWordsOnTagCloud { get; set; } = -1; // No restrictions

        [Description("Words to ignore")]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor," +
                "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(CsvConverter))]
        public List<string> WordsToIgnore { get; set; } = new List<string>();
    }
}