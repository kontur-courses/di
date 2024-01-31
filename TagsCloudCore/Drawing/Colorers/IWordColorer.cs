using System.Drawing;
using TagsCloudCore.Common.Enums;

namespace TagsCloudCore.Drawing.Colorers;

public interface IWordColorer
{
    public WordColorerAlgorithm AlgorithmName { get; }
    public Color GetWordColor(string word, int wordFrequency);

    public bool Match(WordColorerAlgorithm algorithmName)
    {
        return AlgorithmName == algorithmName;
    }
}