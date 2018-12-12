using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using edu.stanford.nlp.tagger.maxent;

namespace TagCloudCreation
{
    public abstract class PartOfSpeechPreparer : IWordPreparer
    {
        public enum PartOfSpeech
        {
            Verb,
            Noun,
            Adjective,
            Modal,
            Untagged,
            Interjection,
            Preposition
        }

        private protected readonly MaxentTagger Tagger;

        private protected readonly Dictionary<string, PartOfSpeech> TagMapping = new Dictionary<string, PartOfSpeech>
        {
            ["VB"] = PartOfSpeech.Verb, ["VBD"] = PartOfSpeech.Verb, ["VBG"] = PartOfSpeech.Verb,
            ["VBN"] = PartOfSpeech.Verb, ["VBZ"] = PartOfSpeech.Verb, ["JJ"] = PartOfSpeech.Adjective,
            ["JJR"] = PartOfSpeech.Adjective, ["JJS"] = PartOfSpeech.Adjective, ["MD"] = PartOfSpeech.Modal,
            ["NN"] = PartOfSpeech.Noun, ["NNS"] = PartOfSpeech.Noun, ["NNP"] = PartOfSpeech.Noun,
            ["NNPS"] = PartOfSpeech.Noun, ["UH"] = PartOfSpeech.Interjection, ["IN"] = PartOfSpeech.Preposition
        };

        protected PartOfSpeechPreparer()
        {
            var s = Path.DirectorySeparatorChar;
            Tagger = new MaxentTagger($"..{s}..{s}..{s}TagCloudCreation{s}english-bidirectional-distsim.tagger");
        }

        public abstract string PrepareWord(string word, TagCloudCreationOptions options);

        private protected string ProcessWordByTag(string word, Func<PartOfSpeech, string, string> wordTagActor)
        {
            var tagged = Tagger.tagString(word);
            var tag = tagged.Split('_')
                            .Last().TrimEnd();
            if (!TagMapping.TryGetValue(tag, out var partOfSpeech))
                partOfSpeech = PartOfSpeech.Untagged;

            return wordTagActor(partOfSpeech, word);
        }
    }
}
