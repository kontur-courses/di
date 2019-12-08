using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Filters;
using TagsCloudVisualization.WordAnalyzers;

namespace TagsCloudVisualization.Settings
{
    public class TextSettings
    {
        private readonly Dictionary<string, Func<IMorphAnalyzer, IFilter>> filters
            = new Dictionary<string, Func<IMorphAnalyzer, IFilter>>
        {
            {"POS", analyzer => new PartOfSpeechFilter(analyzer)}
        };

        public string PathToFile { get; private set; }

        public string FileExtention { get; private set; }

        public IFilter Filter { get; private set; }

        public TextSettings(string path, string filter, string excludedPartsOfSpeech, 
            string onlyPrintPartOfSpeech, IMorphAnalyzer analyzer)
        {
            PathToFile = path;
            FileExtention = Path.GetExtension(path);
            Filter = filters[filter](analyzer);
            PartOfSpeechFilerSettings(excludedPartsOfSpeech, onlyPrintPartOfSpeech);
        }

        public void PartOfSpeechFilerSettings(string excludedPartsOfSpeech, string onlyPrintPartOfSpeech)
        {
            if (!string.IsNullOrEmpty(onlyPrintPartOfSpeech) && PartOfSpeechFilter.WhiteList.Contains(onlyPrintPartOfSpeech))
            {
                PartOfSpeechFilter.WhiteList.Clear();
                PartOfSpeechFilter.WhiteList.Add(onlyPrintPartOfSpeech);
                return;
            }
            excludedPartsOfSpeech.Split()
                                        .ToList()
                                        .ForEach(elem => PartOfSpeechFilter.WhiteList.Remove(elem));
        }
    }
}
