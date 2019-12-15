using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TagsCloudContainer.Interfaces;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Models;

namespace TagsCloudContainer
{
    public class DefaultWordsFilter : IWordsFilter
    {
        private HashSet<string> excludedTypes;

        public DefaultWordsFilter(IEnumerable<string> excluded)
        {
            excludedTypes = new HashSet<string>();
            foreach (var type in excluded)
            {
                excludedTypes.Add(type);
            }
        }


        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return FilterWords(GetInfoAboutWords(words.ToArray()));
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

        private string WordsToString(string[] words)
        {
            var res = "";
            foreach (var str in words)
            {
                res += $"{str} ";
            }

            return res;
        }

        private List<WordModel> GetInfoAboutWords(string[] words)
        {
            var outputBuilder = new StringBuilder();
            var mst = new Mysteam();
            var res = mst.GetWords(WordsToString(words));

            return res;
        }
    }
}