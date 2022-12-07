using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DeepMorphy;

namespace TagsCloudContainer
{
    public class ParserTxt
    {
        public static Dictionary<string, int> Parse(string path)
        {
            var result = new Dictionary<string, int>();
            var a = File.ReadAllLines(path, Encoding.UTF8);


            foreach (var word in a.Select(s => s.ToLower()))
            {
                if (!result.ContainsKey(word))
                    result[word] = 0;
                result[word]++;
            }
            //exclude

            var morph = new MorphAnalyzer();
            var abc = morph
                .Parse(result.Keys)
                .Where(m => !m.BestTag.Has("союз")
                            && !m.BestTag.Has("предл")
                            && !m.BestTag.Has("мест")
                            && !m.BestTag.Has("межд")
                            && !m.BestTag.Has("част")).Select(m => m.Text).ToHashSet();

            foreach (var key in result.Keys)
            {
                if (!abc.Contains(key))
                    result.Remove(key);
            }

            return result;
        }

        
    }
}
