using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.WordReaders
{
    public interface IWordReader
    {
        public Result TryReadWords(string filename, out string[] words);
    }
}