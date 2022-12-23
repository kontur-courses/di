using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.Infrastructure.WordFontSizeProviders
{
    public class WordConstFontSizeProvider : IWordFontSizeProvider
    {
        public Result<float> GetFontSize(string word)
        {
            return Result.Ok(14F);
        }
    }
}