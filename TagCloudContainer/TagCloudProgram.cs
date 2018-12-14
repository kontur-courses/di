using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudPreprocessor;
using TagsCloudPreprocessor.Preprocessors;
using TagsCloudVisualization;

namespace TagCloudContainer
{
    public class TagCloudProgram
    {
        private readonly Config config;
        private readonly ITextParser parser;
        private readonly IPreprocessor wordsExcluder;
        private readonly IPreprocessor wordsStemer;
        private readonly IFileReader fileReader;
        private readonly ITagCloudVisualization tagCloudVisualization;

        public TagCloudProgram(
            ITagCloudVisualization tagCloudVisualization,
            ITextParser parser,
            IFileReader fileReader,
            IPreprocessor wordsExcluder,
            IPreprocessor wordsStemer,
            Config config)
        {
            this.tagCloudVisualization = tagCloudVisualization;
            this.config = config;
            this.wordsExcluder = wordsExcluder;
            this.wordsStemer = wordsStemer;
            this.fileReader = fileReader;
            this.parser = parser;
        }

        public void SaveTagCloud()
        {
            var words = parser
                .GetWords(fileReader.ReadFromFile(config.InputFile))
                .ToList();

            words = wordsExcluder.PreprocessWords(words);
            words = wordsStemer.PreprocessWords(words);
            
            tagCloudVisualization.SaveTagCloud(
                config.FileName,
                config.OutPath,
                GetFrequencyDictionary(words)
                    .OrderBy(x => x.Value)
                    .Reverse()
                    .Take(config.Count)
                    .ToDictionary(x => x.Key, x => x.Value));
        }

        private Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 1;
                frequencyDictionary[word]++;
            }

            return frequencyDictionary;
        }
    }
}