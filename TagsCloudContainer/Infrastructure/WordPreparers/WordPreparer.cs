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

            var initializeResult = OpenNLPPOSFacade.Initialize();
            if (initializeResult.IsFailed)
                return initializeResult;

            var preparedWords = new List<string>();
            foreach(var word in words.Select(w => w.ToLower()))
            {
                var getTypeResult = OpenNLPPOSFacade.GetWordType(word);
                if (getTypeResult.IsFailed)
                    return getTypeResult.ToResult();

                if (excludedTypes.Contains(getTypeResult.Value))
                    continue;

                preparedWords.Add(word);
            }

            return Result.Ok(preparedWords.ToArray());     
        }
    }
}