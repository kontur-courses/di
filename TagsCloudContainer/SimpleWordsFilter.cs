using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Models;

namespace TagsCloudContainer
{
    public class SimpleWordsFilter : IWordsFilter
    {
        private readonly string[] Words;
        private HashSet<string> excludedTypes = new HashSet<string>() {
            "PR",
            "PART",
            "CONJ"
            };
        
        public SimpleWordsFilter(string[] arr)
        {
            Words = arr;
        }
        
        private string WordsToString()
        {
            var res = "";
            foreach (var str in this.Words)
            {
                res += $"{str} ";
            }

            return res;
        }
        
        public IEnumerable<string> FilterWords()
        {
            return FilterWords(GetInfoAboutWords());
        }

        private List<string> FilterWords(List<WordModel> wordsInfos)
        {
            var res = new List<string>();
            foreach (var wordInfo in wordsInfos)
            {
                var flag = false;
                foreach (var type in excludedTypes)
                {
                    if (wordInfo.SourceWord.Analysis[0].Gr.Contains(type))
                        flag = true;
                }
                if (flag)
                    continue;
                res.Add(wordInfo.SourceWord.Text);
            }

            return res;
        }

        private List<WordModel> GetInfoAboutWords()
        {
            var outputBuilder = new StringBuilder();
            var mst = new Mysteam();
            var res = mst.GetWords(WordsToString());

            return res;
        }
    }

    
}