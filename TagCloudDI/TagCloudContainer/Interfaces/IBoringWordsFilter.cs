using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudContainer.Interfaces
{
    public interface IBoringWordsFilter
    {
        IEnumerable<string> FilterText(string text);

        IEnumerable<string> FilterWords(IEnumerable<string> text);
    }
}