using System;
using ResultProject;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization.WordProcessors
{
    internal class InitialFormWordProcessor : ILiteraryWordProcessor
    {
        private readonly WordList? wordList;
        private readonly string? ErrorMessage;
        
        public InitialFormWordProcessor(string dictName)
        {
            try
            {
                wordList = WordList.CreateFromFiles(dictName);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
        
        public Result<string> ProcessWord(string word)
        {
           return  wordList.AsResult()
                .ValidateNull(ErrorMessage!)
                .Then(x => x?.CheckDetails(word).Root ?? word)
                .RefineError("Can't find dictionary for initial word forms");
        }
    }
}