using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class SimpleWordPreprocessor : IWordPreprocessor
    {
        private IWordFormater[] worsFormaters;

        public SimpleWordPreprocessor(IWordFormater[] worsFormaters)
        {
            this.worsFormaters = worsFormaters;
        }

        public IEnumerable<string> Handle(IEnumerable<string> words)
        {
            if (worsFormaters != null && worsFormaters.Length > 0)
            {
                foreach (var formater in worsFormaters)
                {
                    words = formater.HandleWords(words);
                }
            }

            return words;
        }
    }
}
