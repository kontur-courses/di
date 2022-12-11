using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer.App.Layouter;
using DeepMorphy;

namespace TagsCloudContainer.App.Layouter
{
    public class SimpleTagsExtractor : ITagsExtractor
    {
        public Dictionary<string, int> Text { get; set; }

        public void FindAllTagsInText(string text)
        {
            var textArray = text
                .ToLower()
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            Text = ChooseNotBoringWords(textArray)
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        public string[] ChooseNotBoringWords(string[] text)
        {
            var partsOfSpeech = new[] { "сущ", "прил", "кр_прил", "гл", "инф_гл", "прич", "кр_прич", "деепр", "нареч" };
            var morph = new MorphAnalyzer(withLemmatization: true);
            return morph.Parse(text)
                .Where(tag => partsOfSpeech.Contains(tag.BestTag.Grams.FirstOrDefault()))
                .Select(tag => tag.BestTag.Lemma)
                .Where(tag => tag != null)
                .ToArray();
        }
    }
}
