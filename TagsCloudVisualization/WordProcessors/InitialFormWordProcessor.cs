using System;
using System.IO;
using ResultProject;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization.WordProcessors
{
    internal class InitialFormWordProcessor : ILiteraryWordProcessor
    {
        private readonly WordList? wordList;
        private readonly string? errorMessage;
        
        public InitialFormWordProcessor(string dictName)
        {
            try
            {
                wordList = WordList.CreateFromFiles(dictName);
            }
            catch (FileNotFoundException e)
            {
                errorMessage = "Can't find dictionary for initial word forms";
            }
            // catch (Exception e)
            // {
            //     errorMessage = e.Message;
            // }
        }
        
        public Result<string> ProcessWord(string word)
        {
           return  wordList.AsResult()
                .ValidateNull(errorMessage!)
                .Then(x => x?.CheckDetails(word).Root ?? word);
        }
    }
}