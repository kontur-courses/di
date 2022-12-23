using FluentResults;
using opennlp.tools.postag;
using org.omg.CORBA;
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
        private static POSTaggerME wordTagger;
        private static Result initializeResult;

        private static Dictionary<string, WordType> openNLPTagToWordTypeDictionary = new Dictionary<string, WordType>();

        public static Result Initialize()
        {
            if (initializeResult is not null)
                return initializeResult;

            return initializeResult = Result.Try(() =>
            {
                using var modelIn = new java.io.FileInputStream(Path.Combine(binFolder, "en-pos-maxent.bin"));
                wordTagger = new POSTaggerME(new POSModel(modelIn));

                openNLPTagToWordTypeDictionary.Add(wordTagger.tag(new[] { "animal" })[0], WordType.Noun);
                openNLPTagToWordTypeDictionary.Add(wordTagger.tag(new[] { "take" })[0], WordType.Verb);
                openNLPTagToWordTypeDictionary.Add(wordTagger.tag(new[] { "awesome" })[0], WordType.Adjective);
            }, e => new Error("Initialization failed").CausedBy(e));
        }

        public static Result<WordType> GetWordType(string word)
        {
            if (initializeResult is null)
                return Result.Fail("Word tagger is not initialized");

            return initializeResult.Bind(() =>
            {
                var tag = wordTagger.tag(new[] { word })[0];
                return Result.Ok(openNLPTagToWordTypeDictionary.ContainsKey(tag) ? openNLPTagToWordTypeDictionary[tag] : WordType.Other);
            });
        }
    }
}