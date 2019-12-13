using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApp.ToSizeConverter
{
    public class WordToSizeConverter: IToSizeConverter
    {
        public IEnumerable<Tuple<string, Size>> ConvertToSizes(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                //Console.WriteLine("there");
                //Console.WriteLine(word);
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 0;
                frequencyDictionary[word]++;
            }

            frequencyDictionary.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
            foreach (var n in frequencyDictionary.Keys)
                Console.WriteLine("n" + n + "n");
            var defaultSize = new Size(30, 30);
            var sizes = new List<Tuple<string, Size>>();
            foreach (var word in frequencyDictionary.Keys)
            {
                var size = new Size((int)(defaultSize.Width * word.Length * 0.8), defaultSize.Height);
                //size = new Size(size.Width * frequencyDictionary[word], size.Height * frequencyDictionary[word]);
                sizes.Add(new Tuple<string, Size>(word, size));
            }
            
            return sizes;
        }
    }
}
/*
  public IList<Size> ConvertToSizes(IDictionary<string, int> wordFrequencies)
        {
            var defaultSize = new Size(20, 20);
            var sizes = new List<Size>();
            foreach (var word in wordFrequencies.Keys)
            {
                var size = new Size((int)(defaultSize.Width * word.Length * 0.8), defaultSize.Height);
                size = new Size(size.Width * wordFrequencies[word], size.Height * wordFrequencies[word]);
                sizes.Add(size);
            }

            return sizes;
        }

*/

/*
  var frequencyDictionary = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (!frequencyDictionary.ContainsKey(word))
                frequencyDictionary[word] = 0;
            frequencyDictionary[word]++;
        }

        return frequencyDictionary.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);

    */