using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITokenWeigher
    {
        Token[] Evaluate(IEnumerable<string> words);
    }
}