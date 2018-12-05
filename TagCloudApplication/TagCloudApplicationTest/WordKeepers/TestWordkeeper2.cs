using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudApplication;

namespace TagCloudApplicationTest.WordKeepers
{
    public class TestWordkeeper2 : IWordKeeper
    {
        public Dictionary<string, int> GetWordFrequency(string text)
        {
            return new Dictionary<string, int>()
            {
                {"apple", 20},
                {"banana", 20},
                {"orange", 20},
                {"watermelon", 20},
                {"salat", 20},
            };
        }
    }
}
