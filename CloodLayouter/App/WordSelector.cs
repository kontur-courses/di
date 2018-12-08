using System.Collections.Generic;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class WordSelector : IWordSlector, IWordProvider
    {
        private readonly IStreamReader streamReader;

        public WordSelector(IStreamReader streamReader)
        {
            this.streamReader = streamReader;
        }

        public List<string> GetWords()
        {
            return Select();
        }

        public List<string> Select()
        {
            return streamReader.Read().Where(x => x.Length >= 4).ToList();
        }
    }
}