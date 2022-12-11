using TagsCloudContainer;

namespace TagsCloudWinformsApp;

internal class InputFilesReader : IWordSequenceProvider, IWordFilterProvider
{
    public string? FilterPath = null;

    public string? InputPath = null;

    public IEnumerable<string> WordFilter
    {
        get
        {
            if (FilterPath == null) return new List<string>();
            var wordSeq = new List<string>();
            foreach (var line in File.ReadAllLines(FilterPath)) wordSeq.AddRange(line.Split());
            return wordSeq;
        }
    }

    public IEnumerable<string> WordSequence
    {
        get
        {
            var wordSeq = new List<string>();
            foreach (var line in File.ReadAllLines(InputPath)) wordSeq.AddRange(line.Split());
            return wordSeq;
        }
    }
}