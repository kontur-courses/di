using WeCantSpell.Hunspell;

namespace TagCloudGenerator.TextProcessors
{
    public class BoringWordsTextProcessor : ITextProcessor
    {
        public IEnumerable<string> ProcessText(IEnumerable<string> text)
        {
            var wordList = WordList.CreateFromFiles(
                "../../../Dictionaries/English (American).dic",
                "../../../Dictionaries/English (American).aff");

            foreach (var word in text)
            {
                var details = wordList.CheckDetails(word);
                var wordEntryDetails = wordList[string.IsNullOrEmpty(details.Root) ? word : details.Root];

                if (wordEntryDetails.Length != 0 && wordEntryDetails[0].Morphs.Count != 0)
                {
                    var po = wordEntryDetails[0].Morphs[0];

                    if (po == "po:pronoun" || po == "po:preposition"
                        || po == "po:determiner" || po == "po:conjunction")
                        continue;
                }

                yield return word;
            }
        }
    }
}