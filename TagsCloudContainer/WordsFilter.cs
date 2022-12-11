using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordsFilter
    {
        private static string boringWordsString = "я ты он она оно мы вы они себя мой твой ваш наш свой его ее их этот" +
                                                  " тот такой таков столько сам самый весь всякий каждый любой другой " +
                                                  "иной кто что какой который чей сколько никто ничто некого нечего " +
                                                  "никакой ничей некто нечто некоторый некий несколько и или но а";

        private List<string> boringWords;
        public WordsFilter()
        {
            boringWords = new List<string>();
            boringWords.AddRange(boringWordsString.Split());
        }
        public List<string> GetExcludedWords()
        {
            var re = new List<string>();
            re.AddRange(boringWords);
            return re;
        }

        public void AddExludedWord(string word)
        {
            boringWords.Add(word);
        }
        public void DeleteExludedWord(string word)
        {
            boringWords.Remove(word);
        }
    }
}