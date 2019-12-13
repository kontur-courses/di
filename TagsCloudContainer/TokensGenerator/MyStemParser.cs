using System;
using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper;

namespace TagsCloudContainer.TokensGenerator
{
    public class MyStemParser : ITokensParser
    {
        private Mysteam mystem;

        public MyStemParser(string pathToMyStem = null)
        {
            mystem = new Mysteam();
        }

        public IEnumerable<string> GetTokens(string str)
        {
            if (str == null)
                throw new ArgumentNullException();
            var replace = str.Replace("\r\n", " ");
            return mystem.GetWords(replace).Select(el => el.SourceWord.Analysis.FirstOrDefault()?.Lex ?? el.SourceWord.Text.ToLower());
        }
    }
}