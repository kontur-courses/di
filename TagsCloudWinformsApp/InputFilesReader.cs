using MimeDetective;
using MimeDetective.Definitions;
using MimeDetective.Definitions.Licensing;
using TagsCloudContainer;

namespace TagsCloudWinformsApp;

internal class InputFilesReader : IWordSequenceProvider, IWordFilterProvider
{
    private readonly ContentInspector Inspector;

    private string? filterPath;

    private string? inputPath;
    private bool readFilter = true;
    private bool readInput = true;

    private Result<IEnumerable<string>> wordFilter;

    private Result<IEnumerable<string>> wordSequence;

    public InputFilesReader()
    {
        Inspector = new ContentInspectorBuilder
        {
            Definitions = new CondensedBuilder
            {
                UsageType = UsageType.PersonalNonCommercial
            }.Build()
        }.Build();
    }

    public string? FilterPath
    {
        get => filterPath;
        set
        {
            filterPath = value;
            readFilter = true;
        }
    }

    public string? InputPath
    {
        get => inputPath;
        set
        {
            inputPath = value;
            readInput = true;
        }
    }

    public Result<IEnumerable<string>> WordFilter
    {
        get
        {
            if (!readFilter) return wordFilter;
            if (FilterPath == null) return new Result<IEnumerable<string>>(new List<string>());
            if (!IsTxtFile(FilterPath))
                return new Result<IEnumerable<string>>(new Exception("Filter file is not a common txt file!"));
            var wordFilt = new List<string>();
            foreach (var line in File.ReadAllLines(FilterPath)) wordFilt.AddRange(line.Split());
            wordFilter = new Result<IEnumerable<string>>(wordFilt);
            readFilter = false;
            return wordFilter;
        }
    }

    public Result<IEnumerable<string>> WordSequence
    {
        get
        {
            if (!readInput) return wordSequence;
            if (!IsTxtFile(inputPath))
                return new Result<IEnumerable<string>>(new Exception("Input file is not a common txt file!"));
            var wordSeq = new List<string>();
            foreach (var line in File.ReadAllLines(InputPath)) wordSeq.AddRange(line.Split());
            wordSequence = new Result<IEnumerable<string>>(wordSeq);
            readInput = false;
            return wordSequence;
        }
    }

    private bool IsTxtFile(string path)
    {
        return Inspector.Inspect(path, ContentReader.Default).Length == 0;
    }
}