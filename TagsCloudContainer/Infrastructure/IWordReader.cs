using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public interface IWordReader
    {
        public bool TryReadWords(string filename, out string[] words);
    }
}