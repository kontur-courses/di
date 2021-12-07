using System.Collections.Generic;

namespace TagsCloudVisualization.TextPreparers
{
    public interface ITextPreparer
    {
        public IEnumerable<string> PrepareText(IEnumerable<string> text);
    }
}