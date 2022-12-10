using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepMorphy;
using DeepMorphy.Model;

namespace TagsCloudContainer.Extensions
{
    public static class MorphAnalyzerExtensions
    {
        public static IReadOnlyDictionary<string, string> GetGrams(this MorphAnalyzer morph, string word)
        {
            return morph.Parse(new[] {word}).First().BestTag.GramsDic;
        }
    }
}
