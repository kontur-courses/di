using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer.App.Layouter;
using DeepMorphy;

namespace TagsCloudContainer.App.Layouter
{
    public class TagsExtractor : ITagsExtractor
    {
        private readonly MorphAnalyzer morphAnalyzer;
        private string[] partsOfSpeech;

        public TagsExtractor()
        {
            morphAnalyzer = new MorphAnalyzer(withLemmatization: true);
            partsOfSpeech = new[] { "сущ", "прил", "кр_прил", "гл", "инф_гл", "прич", "кр_прич", "деепр", "нареч" };
        }

        public Dictionary<string, int> FindAllTagsInText(string text)
        {
            var textArray = text
                .ToLower()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return ChooseNotBoringWordsWithSimpleForm(textArray)
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        private string[] ChooseNotBoringWordsWithSimpleForm(string[] text)
        {
            return morphAnalyzer.Parse(text)
                .Where(tag => partsOfSpeech.Contains(tag.BestTag.Grams.FirstOrDefault()))
                .Select(tag => tag.BestTag.Lemma)
                .Where(tag => tag != null)
                .ToArray();
        }    
    }
}
