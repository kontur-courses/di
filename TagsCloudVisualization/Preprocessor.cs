using System.Collections.Generic;
using System.IO;
using NHunspell;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization
{
    public class Preprocessor
    {
        public List<string> Process(string fileName)
        {
            var words = new List<string>();
            // var dicPath = string.Concat(Program.ProjectDirectory, @"\en_US.dic");
            // var affPath = string.Concat(Program.ProjectDirectory, @"\en_US.aff");

            // using var hunspell = new Hunspell(affPath, dicPath);
            using var reader = new StreamReader(fileName);
            
            while (!reader.EndOfStream)
            {
                var word = reader.ReadLine();

                if (string.IsNullOrEmpty(word))
                    continue;

                words.Add(word.ToLower());
            }
            
            return words;
        }
    }
}