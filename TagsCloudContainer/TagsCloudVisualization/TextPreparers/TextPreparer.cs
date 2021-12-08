using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.TextPreparers
{
    public class TextPreparer : ITextPreparer
    {
        private readonly IEnumerable<Func<string, bool>> filters;
        private readonly IEnumerable<Func<string, string>> preparations;

        public TextPreparer(IEnumerable<Func<string, bool>> filters, IEnumerable<Func<string, string>> preparations)
        {
            this.filters = filters;
            this.preparations = preparations;
        }

        public IEnumerable<string> PrepareText(IEnumerable<string> text)
        {
            return text
                .Where(word => !IsFiltered(word))
                .Select(PrepareWord)
                .ToList(); // На случай каких-то ошибок из filters или preparations
        }
        
        private string PrepareWord(string word) =>
            preparations.Aggregate(word, (current, preparation) => preparation(current));

        private bool IsFiltered(string word) => filters.Any(filter => filter(word));
    }
}