using System.Linq;
using MyStemWrapper;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGeneratorExtensions
{
    [Priority(5)]
    [Factorial("TakenPartsOfSpeechFilter")]
    public class TakenPartsOfSpeechFilter : IWordsFilter
    {
        private readonly Settings settings;
        private readonly MyStem stem;

        public TakenPartsOfSpeechFilter(Settings settings)
        {
            this.settings = settings;
            stem = new MyStem { PathToMyStem = Metadata.PathToMyStem, Parameters = "-nli" };
        }

        public string[] Execute(string[] input)
        {
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