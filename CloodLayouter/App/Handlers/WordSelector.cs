using System.Collections.Generic;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App.Handlers
{
    public class WordSelector : IConverter<IEnumerable<string>,IEnumerable<string>>
    {
        public IEnumerable<string> Convert(IEnumerable<string> data)
        {
            return data.Where(x => x.Length > 4);
        }
    }
}