using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class WordSelector : IWordSlector
    {
        private readonly IWordProvider wordProvider;

        public WordSelector(IWordProvider wordProvider)
        {
            this.wordProvider = wordProvider;
        }

        public void Select()
        {
            wordProvider.Words = wordProvider.Words.Where(x => x.Length >= 4).ToList();
        }
    }
}