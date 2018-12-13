using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Formatting
{
    public class FormattingComponent
    {
        private readonly List<IWordsFormatter> formatters;

        public FormattingComponent(IEnumerable<IWordsFormatter> formatters)
        {
            this.formatters = this.formatters.ToList();
        }

        public FormattingComponent(FormattingSettings settings)
        {
            this.formatters = settings.Formatters.ToList();
        }

        public List<string> FormatWords(List<string> words)
        {
            foreach (var formatter in formatters)
            {
                words = formatter.Format(words);
            }

            return words;
        }

        public void AddFormatter(IWordsFormatter formatter)
        {
            formatters.Add(formatter);
        }
    }
}