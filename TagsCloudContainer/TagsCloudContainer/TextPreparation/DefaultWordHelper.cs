using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.TextPreparation
{
    public class DefaultWordHelper : IWordsHelper
    {
        public List<string> GetAllWordsToVisualize(List<string> words)
        {
            if (words == null)
            {
                throw new ArgumentException();
            }

            return SortWordsByPopularity(RemoveAllBoringWords(words.Select(word => word.ToLower()).ToList()));
        }

        private static List<string> RemoveAllBoringWords(List<string> words)
        {
            return words.Where(word => word.Length > 3).ToList();
        }

        private static List<string> SortWordsByPopularity(List<string> words)
        {
            return words.Distinct().OrderByDescending(word => words.Count(w => w.Equals(word))).ToList();
        }
    }
}  