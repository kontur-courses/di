using System.Collections.Generic;

namespace TagsCloudContainer.PreprocessingWords
{
    public interface ICreateProcess
    {
        IEnumerable<string> GetResult(string nameProgram, string arguments);
    }
}