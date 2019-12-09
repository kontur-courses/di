using MyStemWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGeneratorExtensions
{
    public class InitialWordsFormConverter : IWordsConverter
    {
        private readonly MyStem stem;

        public InitialWordsFormConverter() =>
            stem = new MyStem { PathToMyStem = Metadata.PathToMyStem, Parameters = "-nl" };

        public int Priority => 5;

        public string FactorialId => "InitialWordsFormConverter";

        public string[] Execute(string[] input)
        {
            var initialForms = new Dictionary<string, string>();
            foreach (var word in input.Distinct())
            {
                var wordAnalysis = stem.Analysis(word);
                initialForms[word] = wordAnalysis
                    .Split(new[] { "\r\n", "\n", "?", "|" }, StringSplitOptions.RemoveEmptyEntries)
                    .First();
            }
            return input
                .Select(w => initialForms[w])
                .ToArray();
        }
    }
}