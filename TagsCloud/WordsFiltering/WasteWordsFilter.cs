using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace TagsCloud.WordsFiltering
{
    public class WasteWordsFilter : IFilter
    {
        public bool AllowAdjectives { get; set; } = true; //A
        public bool AllowAdverbs { get; set; } = false; //ADV
        public bool AllowPronounAdverb { get; set; } = false; //ADVPRO
        public bool AllowNumeralAdjectives { get; set; } = false; //ANUM
        public bool AllowPronounAdjectives { get; set; } = false; //APRO
        public bool AllowComposites { get; set; } = false; //COM
        public bool AllowUnions { get; set; } = false; //CONJ
        public bool AllowInterjections { get; set; } = false; //ITNJ
        public bool AllowNumerals { get; set; } = false; //NUM
        public bool AllowParticles { get; set; } = false; //PART
        public bool AllowPrepositions { get; set; } = false; //PR
        public bool AllowNouns { get; set; } = true; //S
        public bool AllowPronouns { get; set; } = false; //SPRO
        public bool AllowVerbs { get; set; } = true; //V

        public Func<List<string>, List<string>> FilterFunc => words =>
        {
            var res = new List<string>();
            var grammems = Task.Run(() => GetGrammems(words)).Result;

            var allowDict = new Dictionary<string, bool>
            {
                { "A", AllowAdjectives },
                { "ADV", AllowAdverbs },
                { "ADVPRO", AllowPronounAdverb },
                { "ANUM", AllowNumeralAdjectives },
                { "APRO", AllowPronounAdjectives },
                { "COM", AllowComposites },
                { "CONJ", AllowUnions },
                { "ITNJ", AllowInterjections },
                { "NUM", AllowNumerals },
                { "PART", AllowParticles },
                { "PR", AllowPrepositions },
                { "S", AllowNouns },
                { "SPRO", AllowPronouns },
                { "V", AllowVerbs }
            };

            var separators = new char[] { '=', ',' };
            foreach (var grammem in grammems)
            {
                var grInfo = grammem.Split(separators, 4);
                if (grInfo.Length < 2) continue;
                if (grInfo.Length >= 3 && grInfo[2] == "сокр") continue;
                if (!allowDict.TryGetValue(grInfo[1], out var isAllow) || !isAllow) continue;
                res.Add(grInfo[0].Trim('?'));
            }

            return res;
        };

        private List<string> GetGrammems(List<string> words)
        {
            var input = Path.GetTempFileName();
            File.WriteAllLines(input, words);
            var output = Path.GetTempFileName();

            var asmLocation = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(asmLocation);
            using (var process = new Process())
            {
                process.StartInfo.FileName = $"{path}\\WordsFiltering\\mystem.exe";
                process.StartInfo.Arguments = $"-nldig \"{input}\" \"{output}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.EnableRaisingEvents = true;
                process.Start();
                if (!process.WaitForExit(30000))
                    throw new TimeoutException("'Mystem' operation timeout reached.");
            }

            return new List<string>(File.ReadAllLines(output));
        }
    }
}
