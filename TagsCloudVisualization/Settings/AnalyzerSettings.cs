using System.Collections.Generic;
using TagsCloudVisualization.Infrastructure.Analyzer;

namespace TagsCloudVisualization.Settings
{
    public class AnalyzerSettings
    {
        public bool Lemmatization { get; set; } = false;

        public List<PartSpeech> ExcludedParts { get; set; } = new()
        {
            PartSpeech.Preposition,
            PartSpeech.Pronoun,
            PartSpeech.Interjection,
            PartSpeech.Particle,
            PartSpeech.Unknown
        };

        public List<PartSpeech> SelectedParts { get; set; } = new();
    }
}