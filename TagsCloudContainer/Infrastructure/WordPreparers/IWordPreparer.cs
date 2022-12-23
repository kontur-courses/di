using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.WordPreparers
{
    public interface IWordPreparer
    {
        public Result<string[]> Prepare(IEnumerable<string> words);
    }
}