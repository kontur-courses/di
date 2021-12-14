using WeCantSpell.Hunspell;

namespace Visualization.Preprocessors
{
    public class NHunspeller: IHunspeller
    {
        private readonly WordList wordList;
        
        public NHunspeller(IHunspellerFilesProvider filesProvider)
        {
            wordList = WordList.CreateFromFiles(
                filesProvider.GetDicFile(),
                filesProvider.GetAffFile());
        }
        
        public bool Check(string word)
        {
            return wordList.Check(word);
        }
    }
}