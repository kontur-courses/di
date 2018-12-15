using System.Collections.Generic;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Formatting
{
    public class FormattingSettings
    {
        public readonly List<IWordsFormatter> Formatters;

        public FormattingSettings(IUI ui, IFormattersFactory formattersFactory)
        {
            Formatters = formattersFactory.CreateFormatters(ui);
        }

       
    }
}