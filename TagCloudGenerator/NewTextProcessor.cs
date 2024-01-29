using WeCantSpell.Hunspell;

namespace TagCloudGenerator
{
    public class TextProcessorRemovingBoringWords : TextProcessorWrapper
    {
        public TextProcessorRemovingBoringWords(ITextProcessor textProcessor) : base(textProcessor) { }

        public override IEnumerable<string> ProcessText(IEnumerable<string> text)
        {
            text = textProcessor.ProcessText(text);

            var wordList = WordList.CreateFromFiles(
                "../../../Dictionaries/English (American).dic",
                "../../../Dictionaries/English (American).aff");
            
            foreach (var item in text)
            {               
                var details = wordList.CheckDetails(item);
                var wordEntryDetails = wordList[string.IsNullOrEmpty(details.Root) ? item : details.Root];
              
                if (wordEntryDetails.Length != 0 && wordEntryDetails[0].Morphs.Count != 0)
                {
                    var po = wordEntryDetails[0].Morphs[0];

                    if (po == "po:pronoun" || po == "po:preposition" 
                        || po == "po:determiner" || po == "po:conjunction")
                        continue;
                }
                
                yield return item;
            }
        }
    }
}
