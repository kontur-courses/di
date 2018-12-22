using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using TagsCloudContainer.Generation;
using TagsCloudContainer.Infrastructure.FileManaging;

namespace TagsCloudContainer.GettingTokens
{
    public class Tokenizer : ITokenizer
    {
        public IEnumerable<Token> GetTokens(string text)
        {
            var mystem = new MyStem(new TemporaryFileManager());
            var jsonTextAnalyze = mystem
                .Analyze(text.ToLower())
                .Split(new []{"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => JObject.Parse(s)["analysis"].FirstOrDefault())
                .Where(el => el != default(JToken));

            foreach (var wordAnalyze in jsonTextAnalyze)
                yield return Token.FromJson(wordAnalyze);
        }
    }
}