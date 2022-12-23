using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.WordPreparers
{
    public class WordPreparer : IWordPreparer
    {
        private readonly WordType[] excludedTypes;

        public WordPreparer(WordType[] excludedTypes)
        {
            this.excludedTypes = excludedTypes;
        }

        public Result<string[]> Prepare(IEnumerable<string> words)
        {
            if (words is null)
                return Result.Fail($"nameof{words} is null");

            return Result.OkIf(words is not null, $"nameof{words} is null")
                         .Bind(OpenNLPPOSFacade.Initialize)
                         .Bind(() => GetPreparedWords(words));   
        }

        private Result<string[]> GetPreparedWords(IEnumerable<string> words)
        {
            var preparedWords = new List<string>();
            foreach (var word in words.Select(w => w.ToLower()))
            {
                var result = OpenNLPPOSFacade.GetWordType(word)
                                             .OnSuccess(wt => { if (!excludedTypes.Contains(wt.Value)) preparedWords.Add(word); });
                if (result.IsFailed)
                    return result.ToResult();
            }
            return Result.Ok(preparedWords.ToArray());
        }
    }
}