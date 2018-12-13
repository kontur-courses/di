using System.Collections.Generic;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Formatting
{
    public interface IFormattersFactory
    {
        List<IWordsFormatter> CreateFormatters(IUI ui);
    }
}