using System.Collections.Generic;

namespace TagsCloudLayouters.Contracts
{
    public interface IWordHandler
    {
        public IEnumerable<string> NormilizeAndExcludeBoringWords(IEnumerable<string> sourceWords);
    }
}