using ResultProject;

namespace TagsCloudVisualization.WordProcessors
{
    public interface IWordProcessor
    {
        Result<string> ProcessWord(string word);
    }
}