using TagsCloudContainer;

namespace TagsCloudWinformsApp;

internal class InputFilesReader : IWordSequenceProvider, IWordFilterProvider
{
    public string? FilterPath = null;

    public string? InputPath = null;

    public Result<IEnumerable<string>> WordFilter
    {
        get
        {
            if (FilterPath == null) return new(new List<string>());
            var wordSeq = new List<string>();
            foreach (var line in File.ReadAllLines(FilterPath)) wordSeq.AddRange(line.Split());
            return new Result<IEnumerable<string>>(wordSeq);
        }
    }

    public Result<IEnumerable<string>> WordSequence
    {
        get
        {
            var wordSeq = new List<string>();
            foreach (var line in File.ReadAllLines(InputPath)) wordSeq.AddRange(line.Split());
            return new Result<IEnumerable<string>>(wordSeq);
        }
    }
}