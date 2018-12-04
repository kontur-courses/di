using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudApplication;

namespace TagCloudApplicationTest.WordKeepers
{
    public class TestWordKeeper : IWordKeeper
    {
        public Dictionary<string, int> GetWordFrequency(string text)
        {
            return new Dictionary<string, int>()
            {
                {"apple", 34},
                {"banana", 17},
                {"orange", 14},
                {"watermelon", 15},
                {"qiwi", 5},
                {"salat", 15},
            };
        }
    }
}
