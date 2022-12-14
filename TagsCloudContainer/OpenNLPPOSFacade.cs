using opennlp.tools.postag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    internal static class OpenNLPPOSFacade
    {
        private const string binFolder = "OpenNLPBins";
        private static readonly POSTaggerME wordTagger;

        private static readonly Dictionary<string, WordType> openNLPTagToWordTypeDictionary = new Dictionary<string, WordType>();

        static OpenNLPPOSFacade()
        {
            using var modelIn = new java.io.FileInputStream(Path.Combine(binFolder, "en-pos-maxent.bin"));
            wordTagger = new POSTaggerME(new POSModel(modelIn));

            openNLPTagToWordTypeDictionary.Add(wordTagger.tag(new[] { "animal" })[0], WordType.Noun);
            openNLPTagToWordTypeDictionary.Add(wordTagger.tag(new[] { "take" })[0], WordType.Verb);
            openNLPTagToWordTypeDictionary.Add(wordTagger.tag(new[] { "awesome" })[0], WordType.Adjective);
        }

        public static WordType GetWordType(string word)
        {
            var tag = wordTagger.tag(new[] { word })[0];
            return openNLPTagToWordTypeDictionary.ContainsKey(tag) ? openNLPTagToWordTypeDictionary[tag] : WordType.Other;
        }
    }
}