using WeCantSpell.Hunspell;

namespace Visualization.Preprocessors
{
    public class NHunspeller: IHunspeller
    {
        private WordList wordList;
        
        public NHunspeller()
        {
            wordList = WordList.CreateFromFiles("Russian.dic", "Russian.aff");
        }
        
        public bool Check(string word)
        {
            return wordList.Check(word);
        }
    }
}