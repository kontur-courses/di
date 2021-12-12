using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITokenWeigher
    {
        Token[] Evaluate(string[] words);
    }
}