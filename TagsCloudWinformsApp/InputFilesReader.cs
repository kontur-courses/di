using TagsCloudContainer;

namespace TagsCloudWinformsApp;

internal class InputFilesReader : IWordSequenceProvider, IWordFilterProvider
{
    public string? FilterPath{
        get => filterPath;
        set
        {
            filterPath = value;
            readFilter = true;
        }
    }

    private string? filterPath = null;
    private bool readFilter = true;

    public string? InputPath
    {
        get => inputPath;
        set
        {
            inputPath = value;
            readInput = true;
        }
    }

    private string? inputPath = null;
    private bool readInput = true;

    public Result<IEnumerable<string>> WordFilter
    {
        get
        {
            if (!readFilter) return wordFilter;
            if (FilterPath == null) return new(new List<string>());
            var wordFilt = new List<string>();
            foreach (var line in File.ReadAllLines(FilterPath)) wordFilt.AddRange(line.Split());
            wordFilter = new(wordFilt);
            readFilter = false;
            return wordFilter;
        }
    }

    private Result<IEnumerable<string>> wordFilter;

    public Result<IEnumerable<string>> WordSequence
    {
        get
        {
            if (!readInput) return wordSequence;
            var wordSeq = new List<string>();
            foreach (var line in File.ReadAllLines(InputPath)) wordSeq.AddRange(line.Split());
            wordSequence = new(wordSeq);
            readInput = false;
            return wordSequence;
        }
    }

    private Result<IEnumerable<string>> wordSequence;
}