using ResultProject;

namespace TagsCloudVisualization.WordProcessors
{
    internal interface IWordProcessor
    {
        Result<string> ProcessWord(string word);
    }
}