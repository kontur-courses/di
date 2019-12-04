using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class BasicWordsSelector : IWordsSelector
    {
        private IWordReader reader;
        private WordSetting wordSetting;

        public BasicWordsSelector(IWordReader reader, WordSetting wordSetting)
        {
            this.reader = reader;
            this.wordSetting = wordSetting;
        }
        
        public IEnumerable<LayoutWord> Select()
        {
            var layoutWords = new Dictionary<string, LayoutWord>();
            foreach (var word in reader.ReadWords())
            {
                var clearWord = SelectWord(word);
                if(clearWord is null)
                    continue;
                if (layoutWords.ContainsKey(clearWord))
                    layoutWords[clearWord].Add();
                else
                    layoutWords[clearWord] = new LayoutWord(clearWord, wordSetting.Brush, wordSetting.Font);                
            }
            
            return layoutWords.Values;
        }

        private string SelectWord(string word)
        {
            return word.Length < 4 ? null : word.ToLower();
        }
    }
}