using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudContainer.Interfaces
{
    public interface IFrequencySorter
    {
        public Dictionary<string, int> GetSortedWordsWithFrequancies(Dictionary<string, int> original);
    }
}
