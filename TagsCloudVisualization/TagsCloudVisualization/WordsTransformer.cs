using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NHunspell;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class WordsTransformer : IWordsTransformer
    {
        public List<string> GetStems(List<string> words)
        {
            var stems = new List<string>();
            var path = Application.StartupPath;
            path = path.Substring(0, path.IndexOf("bin", StringComparison.Ordinal)) + "NHunSpell\\";

            using (var hunspell = new Hunspell($"{path}en_us.aff", $"{path}en_us.dic"))
            {
                foreach (var word in words)
                    stems.Add(hunspell.Stem(word).FirstOrDefault() ?? word);
            }

            return stems;
        }
    }
}