using System.Collections.Generic;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Formatting
{
    public class FormattersFactory : IFormattersFactory
    {
        public List<IWordsFormatter> CreateFormatters(IUI ui)
        {
            var result = new List<IWordsFormatter> {new ToLowerCaseFormatter(), new ToInitFormFormatter()};
            return result;
        }
    }
}