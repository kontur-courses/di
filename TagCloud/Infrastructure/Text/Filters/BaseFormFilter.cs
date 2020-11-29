using System.Collections.Generic;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class BaseFormFilter : IFilter<string>
    {
        public IEnumerable<string> Filter(IEnumerable<string> tokens)
        {
            throw new System.NotImplementedException();
        }
    }
}