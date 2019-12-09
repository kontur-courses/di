using System.Linq;
using MyStemWrapper;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGeneratorExtensions
{
    public class TakenPartsOfSpeechFilter : IWordsFilter
    {
        private readonly Settings settings;
        private readonly MyStem stem;

        public TakenPartsOfSpeechFilter(Settings settings)
        {
            this.settings = settings;
            stem = new MyStem { Parameters = "-nli" };
        }

        public int Priority => 5;

        public string FactorialId => "TakenPartsOfSpeechFilter";

        public string[] Execute(string[] input)
        {
            stem.PathToMyStem = Metadata.PathToMyStem;
            var takeWords = input
                .Distinct()
                .Where(w =>
                {
                    var wordAnalysis = stem.Analysis(w);
                    return settings.TakenPartsOfSpeech.Any(e => wordAnalysis.Contains(e.ToUpper()));
                })
                .ToHashSet();
            return input
                .Where(w => takeWords.Contains(w))
                .ToArray();
        }
    }
}