using System.Collections.Generic;

namespace TagCloud.Templates
{
    public interface ITemplateCreator
    {
        public ITemplate GetTemplate(IEnumerable<string> words);
    }
}