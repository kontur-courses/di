using System;
using System.Collections.Generic;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization.Settings
{
    public class AnalyzerSettings
    {
        public bool Lemmatization { get; set; } = false;

        public List<PartSpeech> ExcludedParts { get; set; } = new(){ PartSpeech.Noun };

        public List<PartSpeech> SelectedParts { get; set; } = new();
    }

}