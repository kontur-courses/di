using System.Collections.Generic;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class WordSelector : IWordSlector, IProvider<IEnumerable<string>>
    {
        private readonly IProvider<IEnumerable<string>> wordProvider;

        public WordSelector(IProvider<IEnumerable<string>> wordProvider)
        {
            this.wordProvider = wordProvider;
        }

        public List<string> Select()
        {
            return wordProvider.Get().Where(x => x.Length >= 4).ToList();
        }

        public IEnumerable<string> Get()
        {
            return Select();
        }
    }
}