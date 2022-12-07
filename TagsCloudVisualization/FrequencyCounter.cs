using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    internal static class FrequencyCounter
    {
        public static Dictionary<string, int> GetFrequency(string[] elements)
        {
            var frequency = new Dictionary<string, int>();
            
            foreach (var element in elements)
            {
                if (!frequency.ContainsKey(element))
                {
                    frequency.Add(element, 1);
                }
                else
                {
                    frequency[element]++;
                }
            }

            return frequency;
        }
    }
}
